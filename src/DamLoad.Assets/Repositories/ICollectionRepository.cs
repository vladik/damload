using DamLoad.Assets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DamLoad.Assets.Repositories
{
    public interface ICollectionRepository
    {
        Task<List<CollectionEntity>> GetAllAsync();
        Task AddAsync(CollectionEntity collection);
        Task RenameAsync(Guid collectionId, string newName);
        Task DeleteAsync(Guid collectionId);
        Task UpdateSortOrderAsync(Guid collectionId, int newSortOrder);
    }
}
