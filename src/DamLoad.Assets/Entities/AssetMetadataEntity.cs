using DamLoad.Core.Base.Entities;
using System;

namespace DamLoad.Assets.Entities
{
    public class AssetMetadataEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssetId { get; set; }
        public string MetaKey { get; set; } = string.Empty;
        public string MetaValue { get; set; } = string.Empty;
    }
}
