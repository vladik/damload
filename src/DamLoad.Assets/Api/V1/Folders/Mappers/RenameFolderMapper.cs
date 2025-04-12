using DamLoad.Assets.Api.V1.Folders.Requests;
using DamLoad.Assets.Api.V1.Folders.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Folders.Mappers;

public class RenameFolderMapper : Mapper<RenameFolderRequest, RenameFolderResponse, FolderEntity>
{
    public override RenameFolderResponse FromEntity(FolderEntity e) => new()
    {
        Id = e.Id,
        Name = e.Name
    };
}