using ILGPU;
using ILGPU.Runtime;
using Magimage.Enums;
using Magimage.Shaders.ColorInversionPixelShader;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Magimage.Filters
{
    class NegativeFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public ColorInversionPixelShaderType ShaderType { get; private set; }

        public NegativeFilter(Image<Rgba32> image, ColorInversionPixelShaderType shaderType)
        {
            Image = image;
            ShaderType = ShaderType;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            Action<Index, ArrayView<Rgba32>> shadingPerformer = null;

            switch(ShaderType)
            {
                case ColorInversionPixelShaderType.ColorInversionByBlue:
                    shadingPerformer = BlueColorInversionPixelShader.PerformShading;
                    break;
                case ColorInversionPixelShaderType.ColorInversionByGreen:
                    shadingPerformer = GreenColorInversionPixelShader.PerformShading;
                    break;
                case ColorInversionPixelShaderType.ColorInversionByRed:
                    shadingPerformer = RedColorInversionPixelShader.PerformShading;
                    break;
                case ColorInversionPixelShaderType.FullInversionByBlue:
                    shadingPerformer = FullColorInversionPixelShader.PerformShading;
                    break;
            }

            var kernel = device.LoadAutoGroupedStreamKernel<Index, ArrayView<Rgba32>>(shadingPerformer);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(size, buffer);
                device.Synchronize();

                pixelArray = buffer.GetAsArray();
                Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
    }
}
