using DamLoad.Assets.Api.V1.Collections.Responses;
using DamLoad.Assets.Entities;
using FastEndpoints;

namespace DamLoad.Assets.Api.V1.Collections.Mappers
{
    public class CollectionListMapper : ResponseMapper<List<CollectionResponse>, List<CollectionEntity>>
    {
        public override List<CollectionResponse> FromEntity(List<CollectionEntity> entities) =>
            entities.Select(e => new CollectionResponse
            {
                Id = e.Id,
                Name = e.Name,
                SortOrder = e.SortOrder
            }).ToList();
    }
}
