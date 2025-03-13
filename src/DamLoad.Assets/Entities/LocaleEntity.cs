using DamLoad.Core.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Assets.Entities
{
    public class LocaleEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssetId { get; set; }
        public string Locale { get; set; } = string.Empty; 
        public string MetaKey { get; set; } = string.Empty; 
        public string MetaValue { get; set; } = string.Empty;
    }
}
