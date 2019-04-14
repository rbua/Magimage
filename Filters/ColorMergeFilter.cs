using ILGPU;
using ILGPU.Runtime;
using Magimage.Filters.Helpers;
using Magimage.Shaders.ColorMergePixelShaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;

namespace Magimage.Filters
{
    internal class ColorMergeFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public Image<Rgba32> SecondImage { get; private set; }
        public float FirstimagePercent { get; private set; }

        public FullColorMergePixelShader Shader { get; private set; }

        public ColorMergeFilter(Image<Rgba32> firstImage, Image<Rgba32> secondImage, FullColorMergePixelShader pixelShader, float firstImagePercent)
        {
            CheckSizes(firstImage, secondImage);

            Image = firstImage;
            SecondImage = secondImage;
            Shader = pixelShader;
            FirstimagePercent = firstImagePercent;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            CheckSizes(Image, SecondImage);

            var kernel = device.LoadAutoGroupedStreamKernel<Index, ArrayView<Rgba32>, ArrayView<Rgba32>, float>
                (Shader.PerformShading);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var firstImageBuffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                firstImageBuffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                using (var secondImageBuffer = device.Allocate<Rgba32>(SecondImage.Width * SecondImage.Height))
                {
                    secondImageBuffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                    kernel(size, firstImageBuffer, secondImageBuffer, FirstimagePercent);
                    device.Synchronize();

                    pixelArray = firstImageBuffer.GetAsArray();

                    Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
                }
            }

            return Image;
        }

        private void CheckSizes(Image<Rgba32> firstImage, Image<Rgba32> secondImage)
        {
            if (secondImage.Width < firstImage.Width ||
                    secondImage.Height < firstImage.Height)
            {
                throw new InvalidOperationException($"Image with height {secondImage.Height} " +
                    $"and width {secondImage.Width} can not be merged with image with height {firstImage.Height}" +
                        $" and width {firstImage.Width}. " +
                        $"Images should be equals or seconImage slould be bigger");
            }
        }

    }
}
