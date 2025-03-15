using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class AssetMetadataEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssetId { get; set; }
        public string Locale { get; set; } = null!;
        public string DataKey { get; set; } = string.Empty;
        public string DataValue { get; set; } = string.Empty;
    }
}
