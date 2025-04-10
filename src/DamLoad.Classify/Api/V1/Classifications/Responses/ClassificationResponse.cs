namespace DamLoad.Classify.Api.V1.Classifications.Responses
{
    public class ClassificationResponse
    {
        public Guid Id { get; set; }
        public string ResourceId { get; set; } = null!;
        public Guid ClassifierId { get; set; }
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
