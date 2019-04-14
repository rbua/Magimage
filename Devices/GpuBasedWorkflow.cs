using ILGPU;
using ILGPU.Runtime;
using Magimage.Devices.Interfaces;
using Magimage.Filters;
using System.Linq;

namespace Magimage.Devices
{
    internal class GpuBasedWorkflow : IComputingProcess
    {
        public void AddFilter(IImageFilter filter)
        {
            using (var context = new Context())
            {
                var acceleratorId = Accelerator.Accelerators.Where(a =>
                    a.AcceleratorType == AcceleratorType.Cuda)
                        .FirstOrDefault();

                using (var accelerator = Accelerator.Create(context, acceleratorId))
                {
                    filter.PerformFilter(accelerator);
                }

            }
        }
    }
}