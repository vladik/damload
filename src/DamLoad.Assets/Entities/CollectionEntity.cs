using DamLoad.Core.Base.Entities;

namespace DamLoad.Assets.Entities
{
    public class CollectionEntity : IBaseEntity, ISortableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; } = 0;
    }
}
