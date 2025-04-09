using DamLoad.Classify.Api.V1.Classifiers.Requests;
using DamLoad.Classify.Api.V1.Classifiers.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifiers.Mappers
{
    public class ClassifierMapper : Mapper<CreateClassifierRequest, ClassifierResponse, ClassifierEntity>
    {
        public override ClassifierEntity ToEntity(CreateClassifierRequest r) => new()
        {
            SchemeId = r.SchemeId,
            Slug = r.Slug,
            Label = r.Label,
            Properties = r.Properties,
            SortOrder = r.SortOrder
        };

        public override ClassifierResponse FromEntity(ClassifierEntity e) => new()
        {
            Id = e.Id,
            SchemeId = e.SchemeId,
            Slug = e.Slug,
            Label = e.Label,
            Properties = e.Properties,
            SortOrder = e.SortOrder,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        };
    }
}
