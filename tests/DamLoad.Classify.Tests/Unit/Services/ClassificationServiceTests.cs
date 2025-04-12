using DamLoad.Classify.Entities;
using DamLoad.Classify.Repositories;
using DamLoad.Classify.Services;
using Xunit;

namespace DamLoad.Classify.Tests.Unit.Services;

public class ClassificationServiceTests
{
    private class FakeRepo : IClassificationRepository
    {
        public List<ClassificationEntity> Stored = new();

        public Task AddAsync(ClassificationEntity classification)
        {
            Stored.Add(classification);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id) => Task.CompletedTask;

        public Task DeleteByResourceAndClassifierAsync(string resourceId, Guid classifierId)
        {
            Stored.RemoveAll(c => c.ResourceId == resourceId && c.ClassifierId == classifierId);
            return Task.CompletedTask;
        }

        public Task<List<ClassificationEntity>> GetByResourceIdAsync(string resourceId) =>
            Task.FromResult(Stored.FindAll(c => c.ResourceId == resourceId));

        public Task<List<ClassificationEntity>> GetByClassifierSlugAsync(string slug) =>
            Task.FromResult(new List<ClassificationEntity>());

        public Task<List<ClassificationEntity>> GetByClassifierIdAsync(Guid classifierId) =>
            Task.FromResult(Stored.FindAll(c => c.ClassifierId == classifierId));
    }

    [Fact]
    public async Task AssignAsync_Stores_All_ClassifierIds()
    {
        // Arrange
        var fakeRepo = new FakeRepo();
        var service = new ClassificationService(fakeRepo);
        var resourceId = "res-1";
        var classifierIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = await service.AssignAsync(resourceId, classifierIds);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, id => Assert.Contains(fakeRepo.Stored, c => c.ClassifierId == id));
    }

    [Fact]
    public async Task RemoveAsync_Removes_Specific_Classification()
    {
        // Arrange
        var fakeRepo = new FakeRepo();
        var service = new ClassificationService(fakeRepo);
        var resourceId = "res-1";
        var idToRemove = Guid.NewGuid();
        await service.AssignAsync(resourceId, new List<Guid> { idToRemove, Guid.NewGuid() });

        // Act
        await service.RemoveAsync(resourceId, new List<Guid> { idToRemove });

        // Assert
        Assert.DoesNotContain(fakeRepo.Stored, c => c.ClassifierId == idToRemove);
    }
}
