namespace DamLoad.Classify.Api.V1.Classifications.Responses;

public class GetClassificationsByResourceResponse
{
    public List<Guid> ClassifierIds { get; set; } = new();
}