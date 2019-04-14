using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    internal class BlackAndWhitePixelShader : IBlackAndWhitePixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
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
