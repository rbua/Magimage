using ILGPU;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Shaders.FrameShaders
{
    public static class CircleFramePixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image, Rgba32 frameColor, int radius, Size size)
        {
            int circleCentreX = size.Width / 2;
            int circleCentreY = size.Height / 2;
            var point = Index2.ReconstructIndex(index.X, new Index2(size.Width, size.Height));

            if (((point.X - circleCentreX) * (point.X - circleCentreX) + (point.Y - circleCentreY) * (point.Y - circleCentreY)) > radius * radius)
            {
                image[(point.Y * size.Width) + point.X] = frameColor;
            }
        }
    }
}
