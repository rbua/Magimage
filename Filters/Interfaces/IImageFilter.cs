using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;

namespace Magimage.Filters
{
    public interface IImageFilter
    {
        Image<Rgba32> Image { get; }
        Image<Rgba32> PerformFilter(long flatIndex);
    }
}
