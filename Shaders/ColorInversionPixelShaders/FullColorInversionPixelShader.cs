using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    internal class FullColorInversionPixelShader : IColorInversionPixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            image[index] = new Rgba32
            {
                R = (byte)~image[index].R,
                G = (byte)~image[index].G,
                B = (byte)~image[index].B,
                A = image[index].A
            };
        }
    }
}
