namespace DamLoad.Classify.Api.V1.Classifiers.Responses
{
    public class ClassifierResponse
    {
        public Guid Id { get; set; }
        public Guid SchemeId { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string? Label { get; set; }
        public string Properties { get; set; } = "{}";
        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
