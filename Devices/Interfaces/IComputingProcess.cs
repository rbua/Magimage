using Magimage.Filters;
using Magimage.Filters.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Devices.Interfaces
{
    public interface IComputingProcess
    {
        void AddFilter(ImageFilter filter);
    }
}
