using ILGPU;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.BlurShaders
{
    class GauissianBlur
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image, int radius, Size size)
        {
            int circleCentreX = size.Width / 2;
            int circleCentreY = size.Height / 2;
            var point = Index2.ReconstructIndex(index.X, new Index2(size.Width, size.Height));

            long R = 0;
            long G = 0;
            long B = 0;

            for (int i = 0; i < radius * 2; i++)
            {
                for (int j = 0; j < radius * 2; j++)
                {
                    if (((point.X - circleCentreX) * (point.X - circleCentreX) + (point.Y - circleCentreY) * (point.Y - circleCentreY)) > radius * radius)
                    {
                        var currentPixel = image[(point.Y * size.Width) + point.X];
                        R += currentPixel.R;
                        G += currentPixel.G;
                        B += currentPixel.B;
                    }
                }
            }

            long area = (long)(Math.PI * (radius * radius));

            image[(point.Y * size.Width) + point.X] = new Rgba32((byte)(R /area), (byte)(G /area), (byte)(B/area), 255);
        }
    }
}
