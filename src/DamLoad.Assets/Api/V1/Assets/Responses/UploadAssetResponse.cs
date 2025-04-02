namespace DamLoad.Assets.Api.V1.Assets.Responses;

public class UploadAssetResponse
{
    public Guid Id { get; set; }
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = default!;
    public string Status { get; set; } = default!;
}