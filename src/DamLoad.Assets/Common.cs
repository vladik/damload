using DamLoad.Abstractions.Models;
using DamLoad.Assets.Entities;

namespace DamLoad.Assets;

public static class AssetMapper
{
    public static AssetModel ToModel(this AssetEntity entity)
    {
        return new AssetModel
        {
            Id = entity.Id.ToString(),
            PublicId = entity.PublicId,
            Url = entity.Url,
            PublicUrl = entity.PublicUrl,
            Bytes = entity.Bytes,
            Type = entity.Type.ToString(),
            Status = entity.Status.ToString(),
            CreatedAt = entity.CreatedAt
        };
    }
}

