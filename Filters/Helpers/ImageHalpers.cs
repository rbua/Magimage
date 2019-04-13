using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Filters.Helpers
{
    static class ImageHalpers
    {
        public static long GetLinearSize(this Image<Rgba32> image)
        {
            return image.Height * image.Width;
        }

        public static Point GetPointByLinearIndex(this long linearIndex, int heigth, int width)
        {
            var point = new Point();

            point.Y = (int)(linearIndex / width);
            point.X = (int)(linearIndex % width);

            return point;
        }
    }
}
