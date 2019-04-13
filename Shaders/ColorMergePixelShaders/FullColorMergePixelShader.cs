using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.ColorMergePixelShaders
{
    class FullColorMergePixelShader
    {
        public Rgba32 PerformShading(Rgba32 firstColor, Rgba32 secondColor, float firstColorPercent)
        {
            float secondColorPercent = 1.0f - firstColorPercent;

            return new Rgba32
            {
                R = (byte)((firstColor.R * firstColorPercent) + (secondColor.R * secondColorPercent)),
                G = (byte)((firstColor.G * firstColorPercent) + (secondColor.G * secondColorPercent)),
                B = (byte)((firstColor.B * firstColorPercent) + (secondColor.B * secondColorPercent))
            };
        }
    }
}
