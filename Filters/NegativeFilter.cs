using ILGPU;
using ILGPU.Runtime;
using Magimage.Filters.Helpers;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Primitives;

namespace Magimage.Filters
{
    class NegativeFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; private set; }
        public IPixelShader Shader { get; private set; }

        public NegativeFilter(Image<Rgba32> image, IColorInversionPixelShader pixelShader)
        {
            Image = image;
            Shader = pixelShader;
        }

        public Image<Rgba32> PerformFilter(Accelerator device)
        {
            var kernel = device.LoadAutoGroupedStreamKernel<Index, ArrayView<Rgba32>>(Shader.PerformShading);
            Index size = new Index(Image.Width * Image.Height);

            Rgba32[] pixelArray = Image.GetPixelSpan().ToArray();

            using (var buffer = device.Allocate<Rgba32>(Image.Width * Image.Height))
            {
                buffer.CopyFrom(pixelArray, 0, Index.Zero, pixelArray.Length);

                kernel(size, buffer);
                device.Synchronize();

                pixelArray = buffer.GetAsArray();
                Image = SixLabors.ImageSharp.Image.LoadPixelData<Rgba32>(pixelArray, Image.Width, Image.Height);
            }

            return Image;
        }
    }
}
