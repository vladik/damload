namespace DamLoad.Abstractions.Entities
{
    public interface ISoftDeletedEntity
    {
        DateTime? DeletedAt { get; set; }
    }
}
