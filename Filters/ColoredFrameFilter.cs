using ILGPU;
using ILGPU.Runtime;
using Magimage.Enums;
using Magimage.Shaders.ColorInversionPixelShader;
using Magimage.Shaders.FrameSHaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;

namespace Magimage.Filters
{
    class ColoredFrameFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public ImageFrameType ShaderType { get; private set; }
        public Rgba32 FrameColor { get; private set; }
        public int Radius { get; private set; }


        public ColoredFrameFilter(Image<Rgba32> image, ImageFrameType shaderType, Rgba32 frameColor, int radius)
        {
            Image = image;
            ShaderType = shaderType;
            FrameColor = frameColor;
            Radius = radius;
        }

        public Action<Index, ArrayView<Rgba32>, Rgba32, int, Size> GetShadingPerformer()
        {
            Action<Index, ArrayView<Rgba32>, Rgba32, int, Size> shadingPerformer = null;

            switch (ShaderType)
            {
                case ImageFrameType.Circle:
                    shadingPerformer = CircleFramePixelShader.PerformShading;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{ShaderType} is not allowed shader type");
            }

            return shadingPerformer;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            var shadingPerformer = GetShadingPerformer();

            var kernel = device.LoadAutoGroupedStreamKernel(shadingPerformer);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(size, buffer, FrameColor, Radius, new Size(Image.Width, Image.Height));
                device.Synchronize();

                pixelArray = buffer.GetAsArray();
                Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
    }
}
