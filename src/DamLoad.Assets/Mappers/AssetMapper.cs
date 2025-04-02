using DamLoad.Abstractions.Models;
using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Mappers;

public static class AssetMapper
{
    public static AssetModel ToModel(this AssetEntity e) => new()
    {
        Id = e.Id,
        PublicId = e.PublicId,
        Url = e.Url,
        Bytes = e.Bytes,
        Type = e.Type,
        Status = e.Status,
        ContentType = e.ContentType,
        Extension = e.Extension
    };

    public static AssetEntity ToEntity(this AssetModel m) => new()
    {
        Id = m.Id,
        PublicId = m.PublicId,
        Url = m.Url,
        Bytes = m.Bytes,
        Type = m.Type,
        Status = m.Status,
        ContentType = m.ContentType,
        Extension = m.Extension,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };
    
    
}