using Magimage.Filters;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Devices.Interfaces
{
    public interface IComputingProcess
    {
        void AddFilter(IImageFilter filter);
    }
}
