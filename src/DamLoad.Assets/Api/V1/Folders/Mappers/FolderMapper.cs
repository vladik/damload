using DamLoad.Assets.Api.V1.Folders.Requests;
using DamLoad.Assets.Api.V1.Folders.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Folders.Mappers;

public class FolderMapper : Mapper<CreateFolderRequest, FolderResponse, FolderEntity>
{
    public override FolderEntity ToEntity(CreateFolderRequest r) => new()
    {
        Name = r.Name,
        ParentId = r.ParentId,
        SortOrder = r.SortOrder
    };

    public override FolderResponse FromEntity(FolderEntity e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        ParentId = e.ParentId,
        SortOrder = e.SortOrder
    };
}