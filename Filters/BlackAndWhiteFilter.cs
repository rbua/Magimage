using ILGPU;
using ILGPU.Runtime;
using Magimage.Shaders.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace Magimage.Filters
{
    internal class BlackAndWhiteFilter : IImageFilter
    {
        public Image<Rgba32> Image { get; protected set; }
        public IPixelShader Shader { get; protected set; }

        public BlackAndWhiteFilter(Image<Rgba32> image, IBlackAndWhitePixelShader shader)
        {
            Image = image;
            Shader = shader;
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
