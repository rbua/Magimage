using Magimage.Devices;
using Magimage.Enums;
using Magimage.Filters;
using Magimage.Shaders.ColorInversionPixelShader;
using Magimage.Shaders.ColorMergePixelShaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using System.IO;

namespace Magimage
{
    public static class Start
    {
        public static void DoSomething()
        {
            var image = Image.Load(@"C:\Users\r_bon\Pictures\Camera Roll\testimage.jpg");

            var filter = new ColoredFrameFilter(image, ImageFrameType.Circle, new Rgba32(255,0,0,0), 500);

            //var filter = new NegativeFilter(image, ColorInversionPixelShaderType.ColorInversionByGreen);
            CpuBasedWorkflow workflow = new CpuBasedWorkflow();
            image = workflow.AddFilter(filter);

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\resultimage.png", FileMode.OpenOrCreate))
            {
                image.SaveAsPng(fs);
            }
        }
    }
}
