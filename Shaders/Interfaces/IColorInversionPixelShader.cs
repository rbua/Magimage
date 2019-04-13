using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.Interfaces
{
    interface IColorInversionPixelShader : IPixelShader
    {
        Rgba32 GetInvertedPixel(Rgba32 pixel);
    }
}
