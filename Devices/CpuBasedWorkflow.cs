using ILGPU;
using ILGPU.Runtime;
using Magimage.Devices.Interfaces;
using Magimage.Filters;
using Magimage.Filters.Helpers;
using System;
using System.Linq;
using System.Threading;

namespace Magimage.Devices
{
    internal class CpuBasedWorkflow : IComputingProcess
    {
        public void AddFilter(IImageFilter filter)
        {
            using (var context = new Context())
            {
                var acceleratorId = Accelerator.Accelerators.Where(a =>
                    a.AcceleratorType == AcceleratorType.CPU)
                        .FirstOrDefault();

                using (var accelerator = Accelerator.Create(context, acceleratorId))
                {
                    filter.PerformFilter(accelerator);
                }

            }
        }
    }
}
