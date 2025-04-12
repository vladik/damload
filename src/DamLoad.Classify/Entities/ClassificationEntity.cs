namespace DamLoad.Classify.Entities
{
    public class ClassificationEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ResourceId { get; set; } = null!;
        public Guid ClassifierId { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
