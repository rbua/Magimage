using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.Interfaces
{
    interface IColorMergePixelShader : IPixelShader
    {
        Rgba32 PerformShading(Rgba32 firstColor, Rgba32 secondColor, float firstColorPercent);
    }
}
