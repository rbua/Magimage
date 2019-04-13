﻿using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Shaders
{
    internal class BlackAndWhiteByGreenPixelShader : IBlackAndWhitePixelShader
    {
        public Rgba32 GetPixelBrighness(Rgba32 pixel)
        {
            return PerformShading(pixel);
        }

        public Rgba32 PerformShading(Rgba32 pixel)
        {
            byte pixelBrightness = pixel.G;

            return new Rgba32
            {
                R = pixelBrightness,
                G = pixelBrightness,
                B = pixelBrightness,
                A = pixelBrightness
            };
        }
    }
}