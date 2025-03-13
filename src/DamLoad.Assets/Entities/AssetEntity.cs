using DamLoad.Core.Base.Entities;
using DamLoad.Core.Enums;

namespace DamLoad.Assets.Entities
{
    public class AssetEntity : IEntity, ISoftDeletedEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
        public Guid? FolderId { get; set; }
        public string Filename { get; set; } = null!;
        public AssetType Type { get; set; }
        public long Bytes { get; set; }
        public AssetStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public List<Guid> CollectionIds { get; set; } = new();
        public List<Guid> TagIds { get; set; } = new();
        public List<Guid> MetadataIds { get; set; } = new();
        public List<Guid> CustomDataIds { get; set; } = new();
    }
}
