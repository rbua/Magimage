using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.Interfaces
{
    public interface IPixelShader
    {
        Rgba32 PerformShading(Rgba32 pixel);
    }
}
