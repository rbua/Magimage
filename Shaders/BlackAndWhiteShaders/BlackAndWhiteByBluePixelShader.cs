using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    internal class BlackAndWhiteByBluePixelShader : IBlackAndWhitePixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            byte pixelBrightness = image[index].B;

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