using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DamLoad.Core.Enums;
using System.Diagnostics;
using AssetType = DamLoad.Core.Enums.AssetType;

namespace DamLoad.Transformation.Providers
{
    public class CloudinaryTransformation : ITransformationProvider
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryTransformation(string cloudName, string apiKey, string apiSecret)
        {
            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public bool Supports(AssetType assetType) => assetType == AssetType.Image || assetType == AssetType.Video;

        public async Task<TransformationResult> TransformAsync(TransformationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
                throw new ArgumentException("Input URL is required for FFmpeg transformations");

            string outputFilePath = $"{Path.GetTempPath()}{Guid.NewGuid()}.mp4";
            var arguments = $"-i {request.Url} -vf scale={request.Width}:{request.Height} {outputFilePath}";

            using var process = Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (process == null)
                throw new Exception("Failed to start FFmpeg process");

            await process.WaitForExitAsync();
            return new TransformationResult
            {
                TransformedFilePath = outputFilePath,
                Format = "mp4",
                Width = request.Width,
                Height = request.Height
            };
        }

    }
}
