using System;
using System.Collections.Generic;
using System.Text;
using Magimage.Filters.Helpers;
using Magimage.Shaders.ColorMergePixelShaders;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Filters
{
    class ColorMergeFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public Image<Rgba32> SecondImage { get; private set; }

        public FullColorMergePixelShader Shader { get; private set; }

        public ColorMergeFilter(Image<Rgba32> firstImage, Image<Rgba32> secondImage, FullColorMergePixelShader pixelShader)
        {
            CheckSizes(firstImage, secondImage);

            Image = firstImage;
            SecondImage = secondImage;
            Shader = pixelShader;
        }

        public Image<Rgba32> PerformFilter(long flatIndex)
        {
            return PerformFilter(flatIndex, 0.5f);
        }

        public Image<Rgba32> PerformFilter(long flatIndex, float firstColorPercent)
        {
            Point pixelPosition = flatIndex.GetPointByLinearIndex(Image.Height, Image.Width);

            Rgba32 rgbaFirstImagePixel = Image[pixelPosition.X, pixelPosition.Y];
            Rgba32 rgbaSecondImagePixel = SecondImage[pixelPosition.X, pixelPosition.Y];
            Image[pixelPosition.X, pixelPosition.Y] = Shader.PerformShading(rgbaFirstImagePixel, rgbaSecondImagePixel, firstColorPercent);

            return this.Image;
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
