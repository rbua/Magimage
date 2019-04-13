using Magimage.Devices;
using Magimage.Filters;
using Magimage.Shaders.ColorInversionPixelShader;
using Magimage.Shaders.ColorMergePixelShaders;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using System.Diagnostics;
using System.IO;

namespace Magimage
{
    public static class Start
    {
        public static void DoSomething()
        {
            System.Console.WriteLine("Start");
            System.Console.ReadLine();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var image = Image.Load(@"C:\Users\r_bon\Pictures\Camera Roll\other\iam.jpg");
            var image2 = Image.Load(@"C:\Users\r_bon\Pictures\Camera Roll\other\sviat.jpg");

            var pixelShader = new FullColorMergePixelShader();
            var filter = new ColorMergeFilter(image, image2, pixelShader);
            TaskBasedWorkflow workflow = new TaskBasedWorkflow();
            workflow.AddFilter(filter);

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\resultimage.jpeg", FileMode.OpenOrCreate))
            {
                image.SaveAsJpeg(fs);
            }
            System.Console.WriteLine($"Done in {sw.ElapsedMilliseconds}");
            System.Console.ReadLine();
        }
    }
}
