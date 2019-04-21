using ILGPU;
using ILGPU.Runtime;
using Magimage.Enums;
using Magimage.Primitives;
using Magimage.Shaders.ColorSubstitutionShaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Magimage.Filters
{
    class ColorSubstitutionFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public ColorSubstitutionType ShaderType { get; private set; }
        public ColorRange From { get; private set; }
        public ColorRange To { get; private set; }


        public ColorSubstitutionFilter(Image<Rgba32> image, ColorSubstitutionType shaderType, ColorRange fromRange, ColorRange toRange)
        {
            Image = image;
            ShaderType = shaderType;
            From = fromRange;
            To = toRange;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            var shadingPerformer = GetShadingPerformer();

            var kernel = device.LoadAutoGroupedStreamKernel(shadingPerformer);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(size, buffer, From, To);
                device.Synchronize();

                pixelArray = buffer.GetAsArray();
                Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
        private Action<Index, ArrayView<Rgba32>, ColorRange, ColorRange> GetShadingPerformer()
        {
            Action<Index, ArrayView<Rgba32>, ColorRange, ColorRange> shadingPerformer = null;

            switch (ShaderType)
            {
                case ColorSubstitutionType.SubstitytionByLinearRange:
                    shadingPerformer = ColorSubstitutionPixelShader.PerformShading;
                    break;                
            }

            return shadingPerformer;
        }
    }
}
