namespace DamLoad.Classify.Entities
{
    public class SchemeEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Slug { get; set; } = null!;
        public string? Label { get; set; }
        public bool Editable { get; set; }
        public bool Sortable { get; set; }
        public bool Repeatable { get; set; }
        public bool Hierarchical { get; set; }
        public string Properties { get; set; } = "{}";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
