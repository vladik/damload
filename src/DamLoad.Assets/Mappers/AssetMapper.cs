using DamLoad.Abstractions.Models;
using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Mappers;

public static class GetAssetMapper
{
    public static AssetModel ToModel(this AssetEntity e) => new()
    {
        Id = e.Id,
        VariantOfId = e.VariantOfId,
        PublicId = e.PublicId,
        Url = e.Url,
        Bytes = e.Bytes,
        Type = e.Type,
        Status = e.Status,
        ContentType = e.ContentType,
        Extension = e.Extension,
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
        DeletedAt = e.DeletedAt
    };

    public static AssetEntity ToEntity(this AssetModel m) => new()
    {
        Id = m.Id,
        VariantOfId = m.VariantOfId,
        PublicId = m.PublicId,
        Url = m.Url,
        Bytes = m.Bytes,
        Type = m.Type,
        Status = m.Status,
        ContentType = m.ContentType,
        Extension = m.Extension,
        CreatedAt = m.CreatedAt,
        UpdatedAt = m.UpdatedAt,
        DeletedAt = m.DeletedAt
    };
}