using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class AssetEntity : IEntity, ISoftDeletedEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string PublicId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PublicUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? FolderId { get; set; }
        public string Filename { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "image", "video", "raw"
        public long Bytes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public List<Guid> CollectionIds { get; set; } = new();
        public List<Guid> TagIds { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
        public Dictionary<string, AssetEntityLocale> Locales { get; set; } = new(); // Language Variations
        }
    }
