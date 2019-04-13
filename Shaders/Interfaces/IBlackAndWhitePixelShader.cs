using Magimage.Filters.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.Interfaces
{
    interface IBlackAndWhitePixelShader : IPixelShader
    {
        Rgba32 GetPixelBrighness(Rgba32 pixel);
    }
}
