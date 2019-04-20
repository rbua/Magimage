using ILGPU;
using ILGPU.Runtime;
using Magimage.Enums;
using Magimage.Shaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Magimage.Filters
{
    internal class BlackAndWhiteFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; protected set; }
        public BlackAndWhitePixelShaderType ShaderType { get; protected set; }

        public BlackAndWhiteFilter(Image<Rgba32> image, BlackAndWhitePixelShaderType shaderType)
        {
            Image = image;
            ShaderType = shaderType;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            Action<Index, ArrayView<Rgba32>> shadingPerformer = null;

            switch (ShaderType)
            {
                case BlackAndWhitePixelShaderType.BlackAndWhiteByBlue:
                    shadingPerformer = BlackAndWhiteByBluePixelShader.PerformShading;
                    break;
                case BlackAndWhitePixelShaderType.BlackAndWhiteByGreen:
                    shadingPerformer = BlackAndWhiteByGreenPixelShader.PerformShading;
                    break;
                case BlackAndWhitePixelShaderType.BlackAndWhiteByRed:
                    shadingPerformer = BlackAndWhiteByRedPixelShader.PerformShading;
                    break;
                case BlackAndWhitePixelShaderType.FullBlackAndWhite:
                    shadingPerformer = BlackAndWhitePixelShader.PerformShading;
                    break;
            }


            var kernel = device.LoadAutoGroupedStreamKernel(shadingPerformer);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(size, buffer);
                device.Synchronize();

                var resultPixelArray = buffer.GetAsArray();
                
                Image = SixLabors.ImageSharp.Image.LoadPixelData(resultPixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
    }
}
