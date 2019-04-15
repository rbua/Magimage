using ILGPU;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    public static class GreenColorInversionPixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            image[index] = new Rgba32
            {
                R = image[index].R,
                G = (byte)~image[index].G,
                B = image[index].B,
                A = image[index].A
            };
        }
    }
}
