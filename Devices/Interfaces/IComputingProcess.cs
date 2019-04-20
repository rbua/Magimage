using Magimage.Filters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Devices.Interfaces
{
    public interface IComputingProcess
    {
        Image<Rgba32> AddFilter(IImageFilter filter);
    }
}
