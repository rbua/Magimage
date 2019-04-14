using ILGPU;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders.Interfaces
{
    interface IColorMergePixelShader
    {
        void PerformShading(Index index, ArrayView<Rgba32> firstImage, ArrayView<Rgba32> secondImage, float firstImageColorPercent);
    }
}
