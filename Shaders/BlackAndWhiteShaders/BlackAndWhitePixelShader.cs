using ILGPU;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    public static class BlackAndWhitePixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            int colorsSum = (image[index].R + image[index].G + image[index].B);
            byte pixelBrightness = (byte)(colorsSum / 3);
            
            image[index] = new Rgba32
            { 
                R = pixelBrightness,
                G = pixelBrightness,
                B = pixelBrightness,
                A = pixelBrightness
            };
        }
    }
}
