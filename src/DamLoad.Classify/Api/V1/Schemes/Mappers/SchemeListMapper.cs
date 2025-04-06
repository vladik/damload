using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Mappers
{
    public class SchemeListMapper : ResponseMapper<List<SchemeResponse>, List<SchemeEntity>>
    {
        public override List<SchemeResponse> FromEntity(List<SchemeEntity> entities) =>
            entities.Select(e => new SchemeResponse
            {
                Id = e.Id,
                Slug = e.Slug,
                Label = e.Label,
                Editable = e.Editable,
                Sortable = e.Sortable,
                Repeatable = e.Repeatable,
                Hierarchical = e.Hierarchical,
                Properties = e.Properties
            }).ToList();
    }

}
