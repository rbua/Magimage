using ILGPU;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Shaders.ColorMergePixelShaders
{
    class FullColorMergePixelShader : IColorMergePixelShader
    {
        /// <summary>
        /// Changes firstImage by merging pixels colors with corresponding pixel colors from second image
        /// </summary>
        public void PerformShading(Index index, ArrayView<Rgba32> firstImage, ArrayView<Rgba32> secondImage, float firstImageColorPercent)
        {
            float secondColorPercent = 1.0f - firstImageColorPercent;

            firstImage[index] = new Rgba32
            {
                R = (byte)((firstImage[index].R * firstImageColorPercent) + (secondImage[index].R * secondColorPercent)),
                G = (byte)((firstImage[index].G * firstImageColorPercent) + (secondImage[index].G * secondColorPercent)),
                B = (byte)((firstImage[index].B * firstImageColorPercent) + (secondImage[index].B * secondColorPercent))
            };
        }
    }
}
