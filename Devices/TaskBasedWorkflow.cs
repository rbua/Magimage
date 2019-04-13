using Magimage.Devices.Interfaces;
using Magimage.Filters;
using Magimage.Filters.Helpers;
using Magimage.Filters.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Magimage.Devices
{
    class TaskBasedWorkflow : IComputingProcess
    {
        public void AddFilter(ImageFilter filter)
        {
            Parallel.For(0, filter.Image.GetLinearSize(), i => filter.PerformFilter(i));
        }
    }
}
