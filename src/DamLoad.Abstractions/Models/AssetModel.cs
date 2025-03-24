namespace DamLoad.Abstractions.Models
{
    public class AssetModel
    {
        public string Id { get; set; } = default!;
        public string PublicId { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string PublicUrl { get; set; } = default!;
        public long Bytes { get; set; }
        public string Type { get; set; } = default!; 
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
