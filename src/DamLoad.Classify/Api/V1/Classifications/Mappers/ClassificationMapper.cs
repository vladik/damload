using DamLoad.Classify.Api.V1.Classifications.Requests;
using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Mappers
{
    public class ClassificationMapper : Mapper<CreateClassificationRequest, ClassificationResponse, ClassificationEntity>
    {
        public override ClassificationEntity ToEntity(CreateClassificationRequest r) => new()
        {
            ResourceId = r.ResourceId,
            ClassifierId = r.ClassifierId
        };

        public override ClassificationResponse FromEntity(ClassificationEntity e) => new()
        {
            Id = e.Id,
            ResourceId = e.ResourceId,
            ClassifierId = e.ClassifierId,
            SortOrder = e.SortOrder,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt
        };
    }
}
