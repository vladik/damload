namespace DamLoad.Classify.Api.V1.Classifications.Responses;

public class CreateClassificationResponse
{
    public string ResourceId { get; set; } = default!;
    public List<Guid> AssignedClassifierIds { get; set; } = new();
}