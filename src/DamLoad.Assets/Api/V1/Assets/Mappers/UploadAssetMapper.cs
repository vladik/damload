using DamLoad.Assets.Api.V1.Assets.Requests;
using DamLoad.Assets.Api.V1.Assets.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Assets.Mappers;

public class UploadAssetMapper : Mapper<UploadAssetRequest, UploadAssetResponse, AssetEntity>
{
    public override AssetEntity ToEntity(UploadAssetRequest r)
    {
        var fileName = Path.GetFileName(r.File.FileName ?? string.Empty);
        var extension = Path.GetExtension(fileName)?.TrimStart('.').ToLowerInvariant();

        return new AssetEntity
        {
            Extension = string.IsNullOrWhiteSpace(extension) ? "unk" : extension,
            ContentType = r.File.ContentType ?? "application/octet-stream"
        };
    }
    
    public override UploadAssetResponse FromEntity(AssetEntity e) => new() {
        Id = e.Id,
        PublicId = e.PublicId,
        Url = e.Url,
        Status = e.Status
    };
}
