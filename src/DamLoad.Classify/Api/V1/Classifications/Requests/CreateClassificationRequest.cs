namespace DamLoad.Classify.Api.V1.Classifications.Requests;

public class CreateClassificationRequest
{
    public string ResourceId { get; set; } = default!;
    public List<Guid> ClassifierIds { get; set; } = new();
}