using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    class BlueColorInversionPixelShader : IColorInversionPixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            image[index] = new Rgba32
            {
                R = image[index].R,
                G = image[index].G,
                B = (byte)~image[index].B,
                A = image[index].A
            };
        }
    }
}
