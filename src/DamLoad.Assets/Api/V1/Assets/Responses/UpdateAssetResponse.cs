namespace DamLoad.Assets.Api.V1.Assets.Responses;

public class UpdateAssetResponse
{
    public Guid Id { get; set; }
    public string PublicId { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Status { get; set; } = default!;
}
