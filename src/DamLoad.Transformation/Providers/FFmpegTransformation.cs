using DamLoad.Core.Enums;
using DamLoad.Transformation;
using DamLoad.Transformation.Providers;
using System.Diagnostics;

public class FFmpegTransformation : ITransformationProvider
{
    public bool Supports(AssetType assetType) => assetType == AssetType.Video;

    public async Task<TransformationResult> TransformAsync(TransformationRequest request)
    {
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
            throw new Exception("Failed to start FFmpeg process.");

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
