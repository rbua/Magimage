using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Magimage.Filters
{
    public abstract class ImageFilter
    {
        public Image<Rgba32> Image { get; protected set; }

        protected ImageFilter(Image<Rgba32> image)
        {
            Image = image;
        }

        public abstract Image<Rgba32> PerformFilter(long pixel);
        public abstract Image<Rgba32> PerformFilter(int x, int y);
    }
}
