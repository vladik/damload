using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Api.V1.Assets.Responses;
public class AssetResponse
{
    public Guid Id { get; set; }
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public static AssetResponse FromEntity(AssetEntity e) => new()
    {
        Id = e.Id,
        PublicId = e.PublicId,
        Url = e.Url,
        Status = e.Status
    };
}