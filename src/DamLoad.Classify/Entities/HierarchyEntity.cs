namespace DamLoad.Classify.Entities
{
    public class HierarchyEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
        public int SortOrder { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
