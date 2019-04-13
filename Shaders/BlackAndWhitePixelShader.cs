using Magimage.Filters.Interfaces;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    internal class BlackAndWhitePixelShader : IBlackAndWhitePixelShader
    {
        public Rgba32 GetPixelBrighness(Rgba32 pixel)
        {
            return PerformShading(pixel);
        }

        public Rgba32 PerformShading(Rgba32 pixel)
        {
            int colorsSum = pixel.R + pixel.G + pixel.B;
            byte pixelBrightness = (byte)(colorsSum / 3);

            return new Rgba32
            {
                R = pixelBrightness,
                G = pixelBrightness,
                B = pixelBrightness,
                A = pixelBrightness
            };
        }
    }
}
