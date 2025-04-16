using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Repositories
{
    public interface IAssetRepository : IDatabaseRepository<AssetEntity>, ISortableRepository<AssetEntity>
    {
    }
}
