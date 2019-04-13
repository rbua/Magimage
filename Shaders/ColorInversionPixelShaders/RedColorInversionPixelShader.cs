using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.ColorInversionPixelShader
{
    class RedColorInversionPixelShader : IColorInversionPixelShader
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
                G = pixel.G,
                B = pixel.B,
                A = pixel.A
            };
        }
    }
}
