using Magimage.Filters.Helpers;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Filters
{
    class BlackAndWhiteFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; protected set; }
        public IPixelShader Shader { get; protected set; }

        public BlackAndWhiteFilter(Image<Rgba32> image, IBlackAndWhitePixelShader shader)
        {
            Image = image;
            Shader = shader;
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
