using ILGPU;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.Interfaces
{
    public interface IPixelShader
    {
        void PerformShading(Index index, ArrayView<Rgba32> image);
    }
}
