using DamLoad.Abstractions.Enums;

namespace DamLoad.Abstractions.Models
{
    public class AssetModel
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string PublicUrl { get; set; } = null!;
        public long Bytes { get; set; }
        public AssetType Type { get; set; }
        public string ContentType { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
