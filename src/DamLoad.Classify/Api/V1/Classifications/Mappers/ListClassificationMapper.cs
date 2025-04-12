using DamLoad.Classify.Api.V1.Classifications.Responses;
using DamLoad.Classify.Entities;
using FastEndpoints;

namespace DamLoad.Classify.Api.V1.Classifications.Mappers;

public class ListClassificationMapper : ResponseMapper<GetClassificationsByResourceResponse, List<ClassificationEntity>>
{
    public override GetClassificationsByResourceResponse FromEntity(List<ClassificationEntity> entities) => new()
    {
        ClassifierIds = entities.Select(e => e.ClassifierId).ToList()
    };
}