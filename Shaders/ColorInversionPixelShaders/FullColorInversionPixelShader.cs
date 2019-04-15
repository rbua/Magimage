using ILGPU;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    public static class FullColorInversionPixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image)
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
