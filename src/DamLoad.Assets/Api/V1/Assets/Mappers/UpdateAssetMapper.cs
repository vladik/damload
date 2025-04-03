using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Mappers;

public class UpdateAssetMapper : Mapper<UpdateAssetRequest, UpdateAssetResponse, AssetEntity>
{
    public override UpdateAssetResponse FromEntity(AssetEntity e) => new()
    {
        Id = e.Id,
        PublicId = e.PublicId,
        Url = e.Url,
        Status = e.Status
    };

    public override AssetEntity UpdateEntity(UpdateAssetRequest r, AssetEntity e)
    {
        // You can later move this logic to a service if it grows more complex
        e.Status = r.Status;
        e.UpdatedAt = DateTime.UtcNow;
        return e;
    }
}
