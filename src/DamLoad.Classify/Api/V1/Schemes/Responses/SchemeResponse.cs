namespace DamLoad.Classify.Api.V1.Schemes.Responses
{
    public class SchemeResponse
    {
        public Guid Id { get; set; }
        public string Slug { get; set; } = null!;
        public string? Label { get; set; }
        public bool Editable { get; set; }
        public bool Sortable { get; set; }
        public bool Repeatable { get; set; }
        public bool Hierarchical { get; set; }
        public string Properties { get; set; } = "{}";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
