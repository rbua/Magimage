using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders
{
    class FullColorInversionPixelShader : IColorInversionPixelShader
    {
        public Rgba32 GetInvertedPixel(Rgba32 pixel)
        {
            return PerformShading(pixel);
        }

        public Rgba32 PerformShading(Rgba32 pixel)
        {
            return new Rgba32
            {
                R = (byte)~pixel.R,
                G = (byte)~pixel.G,
                B = (byte)~pixel.B,
                A = pixel.A
            };
        }
    }
}
