using Magimage.Devices.Interfaces;
using Magimage.Filters;
using Magimage.Filters.Helpers;
using System;
using System.Threading;

namespace Magimage.Devices
{
    internal class ThreadBasedWorkflow : IComputingProcess
    {
        public void AddFilter(ImageFilter filter)
        {
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                new Thread(() => filter.PerformFilter(3));
            }

        }
        private ThreadStart CreateThreadTask(ImageFilter filter)
        {
            int processorsCount = Environment.ProcessorCount;
            for (int i = 0; i < processorsCount + 1; i++)
            {
                long length = filter.Image.GetLinearSize();
                long pixelsPerCore = length / processorsCount;
                long fromPixelId = i * pixelsPerCore;


            for (int j = 0; j < length; j++)
                {
                    
                }
            }
            return null;
        }
    }
}
