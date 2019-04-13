using Magimage.Devices;
using Magimage.Filters;
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

            var image = Image.Load(@"C:\Users\r_bon\Pictures\Camera Roll\testimage.jpg");
            TaskBasedWorkflow workflow = new TaskBasedWorkflow();
            workflow.AddFilter(
                new NegativeFilter(
                    image));

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\resultimage.jpeg", FileMode.OpenOrCreate))
            {
                image.SaveAsJpeg(fs);
            }
            System.Console.WriteLine($"Done in {sw.ElapsedMilliseconds}");
            System.Console.ReadLine();

            System.Console.WriteLine("Start");
            System.Console.ReadLine();

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();

            var image2 = Image.Load(@"C:\Users\r_bon\Pictures\Camera Roll\testimage.jpg");
            TaskBasedWorkflow workflow2 = new TaskBasedWorkflow();
            workflow.AddFilter(
                new NegativeFilter(
                    image2));

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\resultimage.jpeg", FileMode.OpenOrCreate))
            {
                image2.SaveAsJpeg(fs);
            }
            System.Console.WriteLine($"Done in {sw2.ElapsedMilliseconds}");
            System.Console.ReadLine();
        }
    }
}
