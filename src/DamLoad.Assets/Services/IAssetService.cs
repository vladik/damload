using DamLoad.Assets.Entities;
using DamLoad.Data.Database;

namespace DamLoad.Assets.Services;

public interface IAssetService : IDatabaseService<AssetEntity>, ISortableService<AssetEntity>
{

}