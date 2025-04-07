using DamLoad.Classify.Api.V1.Schemes.Requests;
using DamLoad.Classify.Api.V1.Schemes.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Schemes.Mappers
{
    public class SchemeMapper : Mapper<CreateSchemeRequest, SchemeResponse, SchemeEntity>
    {
        public override SchemeEntity ToEntity(CreateSchemeRequest r) => new()
        {
            Id = r is UpdateSchemeRequest u ? u.Id : Guid.NewGuid(),
            Slug = r.Slug,
            Label = r.Label,
            Editable = r.Editable,
            Sortable = r.Sortable,
            Repeatable = r.Repeatable,
            Hierarchical = r.Hierarchical,
            Properties = r.Properties
        };

        public override SchemeResponse FromEntity(SchemeEntity e) => new()
        {
            Id = e.Id,
            Slug = e.Slug,
            Label = e.Label,
            Editable = e.Editable,
            Sortable = e.Sortable,
            Repeatable = e.Repeatable,
            Hierarchical = e.Hierarchical,
            Properties = e.Properties,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        };
    }

}
