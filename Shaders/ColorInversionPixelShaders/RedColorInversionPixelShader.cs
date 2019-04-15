using ILGPU;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    public static class RedColorInversionPixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image)
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
