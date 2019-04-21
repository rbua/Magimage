using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Primitives
{
    public struct ColorRange
    {
        public Rgba32 From;
        public Rgba32 To;

        public static Rgba32 Map(ColorRange fromRange, Rgba32 color, ColorRange toRange)
        {
            float redColorPositionInRange = fromRange.From.R / fromRange.To.R;
            float greenColorPositionInRange = fromRange.From.G / fromRange.To.G;
            float blueColorPositionInRange = fromRange.From.B / fromRange.To.B;

            var absoluteColorDifference = GetAbsoluteColorDifference(fromRange.From, fromRange.To);

            return new Rgba32()
            {
                R = (byte)((absoluteColorDifference.R * redColorPositionInRange) + toRange.From.R),
                G = (byte)((absoluteColorDifference.G * greenColorPositionInRange) + toRange.From.G),
                B = (byte)((absoluteColorDifference.B * blueColorPositionInRange) + toRange.From.B),
                A = 255
            };
        }

        public static bool ColorInRange(ColorRange range, Rgba32 color)
        {
            if (color.R >= range.From.R && color.R <= range.To.R &&
                color.G >= range.From.G && color.G <= range.To.G &&
                color.B >= range.From.B && color.B <= range.To.B)
                return true;

            return false;
        }

        private static Rgba32 GetAbsoluteColorDifference(Rgba32 firstColor, Rgba32 secondColor)
        {
            int Red;
            int Green;
            int Blue;

            if (firstColor.R > secondColor.R)
                Red = firstColor.R - secondColor.R;
            else
                Red = firstColor.R - secondColor.R;

            if (firstColor.G > secondColor.G)
                Green = firstColor.G - secondColor.G;
            else
                Green = firstColor.G - secondColor.G;

            if (firstColor.B > secondColor.B)
                Blue = firstColor.B - secondColor.B;
            else
                Blue = firstColor.B - secondColor.B;

            return new Rgba32()
            {
                R = (byte)Red,
                G = (byte)Green,
                B = (byte)Blue
            };
        }
    }
}
