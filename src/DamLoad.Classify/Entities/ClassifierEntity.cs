namespace DamLoad.Classify.Entities
{
    public class ClassifierEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SchemeId { get; set; }
        public string Slug { get; set; } = null!;
        public string? Label { get; set; }
        public string Properties { get; set; } = "{}";
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
