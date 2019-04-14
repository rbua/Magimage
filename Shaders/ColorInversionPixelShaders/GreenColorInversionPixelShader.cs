using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    class GreenColorInversionPixelShader : IColorInversionPixelShader
    {
        public void PerformShading(Index index, ArrayView<Rgba32> image)
        {
            image[index] = new Rgba32
            {
                R = image[index].R,
                G = (byte)~image[index].G,
                B = image[index].B,
                A = image[index].A
            };
        }
    }
}
