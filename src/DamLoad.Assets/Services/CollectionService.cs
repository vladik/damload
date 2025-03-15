using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DamLoad.Assets.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _repository;

        public CollectionService(ICollectionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<CollectionEntity>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task AddAsync(CollectionEntity collection) =>
            await _repository.AddAsync(collection);

        public async Task RenameAsync(Guid collectionId, string newName) =>
            await _repository.RenameAsync(collectionId, newName);

        public async Task DeleteAsync(Guid collectionId) =>
            await _repository.DeleteAsync(collectionId);

        public async Task UpdateSortOrderAsync(Guid collectionId, int newSortOrder) =>
            await _repository.UpdateSortOrderAsync(collectionId, newSortOrder);
    }
}
