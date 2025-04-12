using DamLoad.Classify.Api.V1.Classifications.Requests;
using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Mappers;

public class ClassificationMapper : Mapper<CreateClassificationRequest, CreateClassificationResponse, ClassificationEntity>
{
    public override ClassificationEntity ToEntity(CreateClassificationRequest r) => new()
    {
        Id = Guid.NewGuid(),
        ResourceId = r.ResourceId,
        ClassifierId = Guid.Empty // Placeholder for per-ID creation
    };

    public override CreateClassificationResponse FromEntity(ClassificationEntity e) => new()
    {
        ResourceId = e.ResourceId,
        AssignedClassifierIds = new() { e.ClassifierId }
    };
}