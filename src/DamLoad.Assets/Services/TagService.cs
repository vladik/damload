using DamLoad.Assets.Entities;
using DamLoad.Assets.Repositories;

namespace DamLoad.Assets.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<TagEntity>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task AddAsync(TagEntity tag) =>
            await _repository.AddAsync(tag);

        public async Task RenameAsync(Guid tagId, string newName) =>
            await _repository.RenameAsync(tagId, newName);

        public async Task DeleteAsync(Guid tagId) =>
            await _repository.DeleteAsync(tagId);
    }
}
