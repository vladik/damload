using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;
using DamLoad.Classify.Services;
using Xunit;

namespace DamLoad.Classify.Tests.Unit.Services;

public class ClassifierServiceTests
{
    private class FakeRepo : IClassifierRepository
    {
        public List<ClassifierEntity> Classifiers = new();

        public Task AddAsync(ClassifierEntity classifier)
        {
            Classifiers.Add(classifier);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            Classifiers.RemoveAll(c => c.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<ClassifierEntity>> GetAllAsync() => Task.FromResult(Classifiers);

        public Task<ClassifierEntity?> GetByIdAsync(Guid id) =>
            Task.FromResult<ClassifierEntity?>(Classifiers.Find(c => c.Id == id));

        public Task<ClassifierEntity?> GetBySlugAsync(string slug) =>
            Task.FromResult<ClassifierEntity?>(Classifiers.Find(c => c.Slug == slug));

        public Task<ClassifierEntity?> GetBySchemeIdAndSlugAsync(Guid schemeId, string slug) =>
            Task.FromResult<ClassifierEntity?>(Classifiers.Find(c => c.SchemeId == schemeId && c.Slug == slug));

        public Task<List<ClassifierEntity>> GetBySchemeIdAsync(Guid schemeId) =>
            Task.FromResult(Classifiers.FindAll(c => c.SchemeId == schemeId));

        public Task UpdateAsync(ClassifierEntity classifier) => Task.CompletedTask;
    }

    [Fact]
    public async Task GetBySchemeIdAndSlug_ReturnsCorrectClassifier()
    {
        var repo = new FakeRepo();
        var service = new ClassifierService(repo);
        var schemeId = Guid.NewGuid();
        var classifier = new ClassifierEntity { Id = Guid.NewGuid(), SchemeId = schemeId, Slug = "featured" };
        await service.AddAsync(classifier);

        var result = await service.GetBySchemeIdAndSlugAsync(schemeId, "featured");

        Assert.NotNull(result);
        Assert.Equal("featured", result?.Slug);
    }

    [Fact]
    public async Task Delete_RemovesClassifier()
    {
        var repo = new FakeRepo();
        var service = new ClassifierService(repo);
        var id = Guid.NewGuid();
        await service.AddAsync(new ClassifierEntity { Id = id, Slug = "delete-me" });

        await service.DeleteAsync(id);
        var result = await service.GetByIdAsync(id);

        Assert.Null(result);
    }
}