namespace DamLoad.Classify.Api.V1.Schemes.Requests
{
    public class CreateSchemeRequest
    {
        public string Slug { get; set; } = string.Empty;
        public string? Label { get; set; }
        public bool Editable { get; set; }
        public bool Sortable { get; set; }
        public bool Repeatable { get; set; }
        public bool Hierarchical { get; set; }
        public string Properties { get; set; } = "{}";
    }
}
