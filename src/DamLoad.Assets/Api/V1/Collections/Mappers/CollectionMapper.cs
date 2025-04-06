using DamLoad.Assets.Api.V1.Collections.Requests;
using DamLoad.Assets.Api.V1.Collections.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Mappers
{
    public class CollectionMapper : Mapper<CreateCollectionRequest, CollectionResponse, CollectionEntity>
    {
        public override CollectionResponse FromEntity(CollectionEntity e) => new()
        {
            Id = e.Id,
            Name = e.Name,
            SortOrder = e.SortOrder
        };

        public override CollectionEntity ToEntity(CreateCollectionRequest r) => new()
        {
            Name = r.Name,
            SortOrder = 0
        };

    }
}
