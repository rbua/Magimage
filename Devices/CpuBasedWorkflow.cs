using ILGPU;
using ILGPU.Runtime;
using Magimage.Devices.Interfaces;
using Magimage.Filters;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Linq;

namespace Magimage.Devices
{
    internal class CpuBasedWorkflow : IComputingProcess
    {
        public Image<Rgba32> AddFilter(IImageFilter filter)
        {
            using (var context = new Context())
            {
                var acceleratorId = Accelerator.Accelerators.Where(a =>
                    a.AcceleratorType == AcceleratorType.CPU)
                        .FirstOrDefault();

                using (var accelerator = Accelerator.Create(context, acceleratorId))
                {
                    return filter.PerformFilter(accelerator);
                }
            }
        }
    }
}
