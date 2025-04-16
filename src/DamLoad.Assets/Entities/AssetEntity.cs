using DamLoad.Abstractions.Entities;
using DamLoad.Abstractions.Enums;

namespace DamLoad.Assets.Entities
{
    public class AssetEntity : IEntity, ISoftDeletedEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? VariantOfId { get; set; }
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
        public AssetType Type { get; set; }
        public string ContentType { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public long Bytes { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
    }
}
