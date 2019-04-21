using ILGPU;
using ILGPU.Runtime;
using Magimage.Enums;
using Magimage.Shaders.BlurShaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Filters
{
    public class GauissianBlurFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public GauissianBlurType ShaderType { get; private set; }
        public int Radius { get; private set; }
        public float SimplificationPercent { get; private set; }

        public GauissianBlurFilter(Image<Rgba32> image, GauissianBlurType shaderType, int radius, float simplificationPercent = 0f)
        {
            Image = image;
            ShaderType = shaderType;
            Radius = radius;
            SimplificationPercent = simplificationPercent;
        }

        public Action<Index, ArrayView<Rgba32>, int, Size> GetShadingPerformer()
        {
            Action<Index, ArrayView<Rgba32>, int, Size> shadingPerformer = null;

            switch (ShaderType)
            {
                case GauissianBlurType.SimleGaussianBlur:
                    shadingPerformer = GauissianBlurPixelShader.PerformShading;
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
            Index linearSize = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(linearSize))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(linearSize, buffer, Radius, new Size(Image.Width, Image.Height));
                device.Synchronize();

                pixelArray = buffer.GetAsArray();
                Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
    }
}
