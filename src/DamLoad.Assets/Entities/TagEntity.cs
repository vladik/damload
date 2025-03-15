using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class TagEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
    }
}
