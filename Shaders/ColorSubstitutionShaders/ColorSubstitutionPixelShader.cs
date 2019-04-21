using ILGPU;
using Magimage.Primitives;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.ColorSubstitutionShaders
{
    public static class ColorSubstitutionPixelShader
    {
        public static void PerformShading(Index index, ArrayView<Rgba32> image, ColorRange from, ColorRange to)
        {
            Rgba32 currentPixelColor = image[index];

            if(ColorRange.ColorInRange(from, currentPixelColor))
                image[index] = ColorRange.Map(from, currentPixelColor, to);
        }
    }
}
