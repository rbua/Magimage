using ILGPU;
using ILGPU.Runtime;
using Magimage.Devices.Interfaces;
using Magimage.Filters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Magimage.Devices
{
    internal class AdaptiveWorkflow : IComputingProcess
    {
        public Image<Rgba32> AddFilter(IImageFilter filter)
        {
            using (var context = new Context())
            {
                AcceleratorId acceleratorId = new AcceleratorId();
                if (Accelerator.Accelerators.Where(a => a.AcceleratorType == AcceleratorType.Cuda).Count() != 0)
                {
                    acceleratorId = Accelerator.Accelerators.Where(a =>
                    a.AcceleratorType == AcceleratorType.Cuda)
                        .FirstOrDefault();
                }
                else
                {
                    acceleratorId = Accelerator.Accelerators.Where(a => a.AcceleratorType == AcceleratorType.CPU).FirstOrDefault();
                }

                using (var accelerator = Accelerator.Create(context, acceleratorId))
                {
                    return filter.PerformFilter(accelerator);
                }
            }
        }
    }
}
}