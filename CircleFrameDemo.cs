using Magimage.Devices;
using Magimage.Enums;
using Magimage.Filters;
using SixLabors.ImageSharp;
using System;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage
{
    class CircleFrameDemo
    {
        public static void DoSomething(string path)
        {
            var image = Image.Load(path);

            var radiusByWidth = image.Width / 2;
            var radiusByHeigth = image.Height / 2;
            var radius = 0;

            if (radiusByHeigth > radiusByWidth)
                radius = radiusByWidth;
            if (radiusByHeigth <= radiusByWidth)
                radius = radiusByHeigth;

            // Here you can select only one shader type - ImageFrameType.Circle
            var filter = new ColoredFrameFilter(image, ImageFrameType.Circle, new Rgba32(255,255,255), radius);

            CpuBasedWorkflow workflow = new CpuBasedWorkflow();
            image = workflow.AddFilter(filter);

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\" + Guid.NewGuid() + ".png", FileMode.OpenOrCreate))
            {
                image.SaveAsPng(fs);
            }
        }
    }
}
