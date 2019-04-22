using Magimage.Devices;
using Magimage.Enums;
using Magimage.Filters;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Magimage
{
    class NegativeFilterDemo
    {
        public static void DoSomething(string path)
        {
            var image = Image.Load(path);

            // Here you can select any pixel shader type in ColorInversionPixelShaderType enum
            var filter = new NegativeFilter(image, ColorInversionPixelShaderType.FullInversionByBlue);

            AdaptiveWorkflow workflow = new AdaptiveWorkflow();
            image = workflow.AddFilter(filter);

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\" + Guid.NewGuid() + ".png", FileMode.OpenOrCreate))
            {
                image.SaveAsPng(fs);
            }
        }
    }
}
