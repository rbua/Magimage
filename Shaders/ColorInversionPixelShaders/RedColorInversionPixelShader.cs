using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    class RedColorInversionPixelShader : IColorInversionPixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            image[index] = new Rgba32
            {
                R = (byte)~image[index].R,
                G = image[index].G,
                B = image[index].B,
                A = image[index].A
            };
        }
    }
}
