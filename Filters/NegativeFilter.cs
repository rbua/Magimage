using System;
using Magimage.Filters.Helpers;
using Magimage.Filters.Interfaces;
using Magimage.Shaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Filters
{
    class NegativeFilter : ImageFilter
    {
        public IPixelShader PixelShader { get; private set; }

        public NegativeFilter(Image<Rgba32> image) : base(image)
        {
            PixelShader = new BlackAndWhiteByRedPixelShader();
        }

        public override Image<Rgba32> PerformFilter(long pixel)
        {
            Point pixelPosition = pixel.GetPointByLinearIndex(Image.Height, Image.Width);

            Rgba32 rgbaPixel = Image[pixelPosition.X, pixelPosition.Y];
            Image[pixelPosition.X, pixelPosition.Y] = PixelShader.PerformShading(rgbaPixel);

            return this.Image;
        }

        public override Image<Rgba32> PerformFilter(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
