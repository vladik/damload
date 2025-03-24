using DamLoad.Abstractions.Enums;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DamLoad.Transformation.Providers
{
    public class ImageSharpTransformation : ITransformationProvider
    {
        public string ProviderName => "ImageSharp";
        public bool Supports(AssetType assetType) => assetType == AssetType.Image;

        public async Task<TransformationResult> TransformAsync(TransformationRequest request)
        {
            using var image = await Image.LoadAsync(request.InputStream ?? new MemoryStream());
            image.Mutate(x => x.Resize(request.Width, request.Height));

            var outputStream = new MemoryStream();
            await image.SaveAsJpegAsync(outputStream);
            outputStream.Position = 0;

            return new TransformationResult
            {
                TransformedStream = outputStream, // Now storing the transformed image
                Width = request.Width,
                Height = request.Height,
                Format = "jpeg"
            };
        }
    }

}
