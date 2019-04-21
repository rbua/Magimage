using Magimage.Devices;
using Magimage.Enums;
using Magimage.Filters;
using Magimage.Primitives;
using Magimage.Shaders.ColorInversionPixelShader;
using Magimage.Shaders.ColorMergePixelShaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Diagnostics;
using System.IO;

/// <summary>
/// Replaces white color from the picture by pale blue color
/// </summary>
namespace Magimage
{
    public static class ColorSubstitutionDemo
    {
        public static void DoSomething(string path)
        {
            var image = Image.Load(path);

            var fromRange = new ColorRange()
            {
                From = new Rgba32(250, 255, 255),
                To = new Rgba32(255, 255, 255),
            };
            var toRange = new ColorRange()
            {
                From = new Rgba32(72, 110, 146),
                To = new Rgba32(152, 187, 214),
            };

            var filter = new ColorSubstitutionFilter(image, ColorSubstitutionType.SubstitytionByLinearRange, fromRange, toRange);

            CpuBasedWorkflow workflow = new CpuBasedWorkflow();
            image = workflow.AddFilter(filter);

            using (FileStream fs = new FileStream(@"C:\Users\r_bon\Pictures\Camera Roll\" + Guid.NewGuid() + ".png", FileMode.OpenOrCreate))
            {
                image.SaveAsPng(fs);
            }
        }
    }
}
