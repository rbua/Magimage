using Magimage.Filters.Helpers;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Filters
{
    class NegativeFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public IPixelShader Shader { get; private set; }

        public NegativeFilter(Image<Rgba32> image, IColorInversionPixelShader pixelShader)
        {
            Image = image;
            Shader = pixelShader;
        }

        public Image<Rgba32> PerformFilter(long flatIndex)
        {
            Point pixelPosition = flatIndex.GetPointByLinearIndex(Image.Height, Image.Width);

            Rgba32 rgbaPixel = Image[pixelPosition.X, pixelPosition.Y];
            Image[pixelPosition.X, pixelPosition.Y] = Shader.PerformShading(rgbaPixel);

            return this.Image;
        }
    }
}
