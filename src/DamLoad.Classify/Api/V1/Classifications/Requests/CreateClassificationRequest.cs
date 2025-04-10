namespace DamLoad.Classify.Api.V1.Classifications.Requests
{
    public class CreateClassificationRequest
    {
        public string ResourceId { get; set; } = null!;
        public Guid ClassifierId { get; set; }
    }
}
