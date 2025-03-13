using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class AssetEntityLocale : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssetId { get; set; }
        public string Locale { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
