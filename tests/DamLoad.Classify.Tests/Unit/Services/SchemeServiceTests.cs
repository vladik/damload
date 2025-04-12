using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;
using DamLoad.Classify.Services;
using Xunit;

namespace DamLoad.Classify.Tests.Unit.Services;

public class SchemeServiceTests
{
    private class FakeRepo : ISchemeRepository
    {
        public List<SchemeEntity> Schemes = new();

        public Task AddAsync(SchemeEntity scheme)
        {
            Schemes.Add(scheme);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            Schemes.RemoveAll(s => s.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<SchemeEntity>> GetAllAsync() => Task.FromResult(Schemes);

        public Task<SchemeEntity?> GetByIdAsync(Guid id) =>
            Task.FromResult<SchemeEntity?>(Schemes.Find(s => s.Id == id));

        public Task<SchemeEntity?> GetBySlugAsync(string slug) =>
            Task.FromResult<SchemeEntity?>(Schemes.Find(s => s.Slug == slug));

        public Task UpdateAsync(SchemeEntity scheme) => Task.CompletedTask;
    }

    [Fact]
    public async Task AddAndGetById_WorksCorrectly()
    {
        var repo = new FakeRepo();
        var service = new SchemeService(repo);
        var scheme = new SchemeEntity { Id = Guid.NewGuid(), Slug = "media", Label = "Media" };

        await service.AddAsync(scheme);
        var result = await service.GetByIdAsync(scheme.Id);

        Assert.NotNull(result);
        Assert.Equal("media", result?.Slug);
    }

    [Fact]
    public async Task Delete_RemovesScheme()
    {
        var repo = new FakeRepo();
        var service = new SchemeService(repo);
        var id = Guid.NewGuid();
        await service.AddAsync(new SchemeEntity { Id = id, Slug = "delete-me" });

        await service.DeleteAsync(id);
        var result = await service.GetByIdAsync(id);

        Assert.Null(result);
    }
}