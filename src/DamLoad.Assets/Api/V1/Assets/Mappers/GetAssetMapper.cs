using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Mappers;

public class GetAssetMapper : Mapper<GetAssetRequest, GetAssetResponse, AssetEntity>
{
    public override GetAssetResponse FromEntity(AssetEntity e) => new()
    {
        Id = e.Id,
        PublicId = e.PublicId,
        Url = e.Url,
        Status = e.Status
    };
}