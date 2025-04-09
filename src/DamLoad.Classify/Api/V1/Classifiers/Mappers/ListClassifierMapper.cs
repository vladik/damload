using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Mappers
{
    public class ListClassifierMapper : ResponseMapper<List<ClassifierResponse>, List<ClassifierEntity>>
    {
        public override List<ClassifierResponse> FromEntity(List<ClassifierEntity> entities) =>
            entities.Select(e => new ClassifierResponse
            {
                Id = e.Id,
                SchemeId = e.SchemeId,
                Slug = e.Slug,
                Label = e.Label,
                Properties = e.Properties,
                SortOrder = e.SortOrder,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToList();
    }
}
