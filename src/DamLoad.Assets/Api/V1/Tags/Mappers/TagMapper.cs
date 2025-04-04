using FastEndpoints;
using DamLoad.Assets.Api.V1.Tags.Requests;
using DamLoad.Assets.Api.V1.Tags.Responses;
using DamLoad.Assets.Entities;

namespace DamLoad.Assets.Api.V1.Tags.Mappers;

public class TagMapper : Mapper<CreateTagRequest, TagResponse, TagEntity>
{
    public override TagEntity ToEntity(CreateTagRequest r) => new() { Name = r.Name };

    public override TagResponse FromEntity(TagEntity e) => new() { Id = e.Id, Name = e.Name };
}