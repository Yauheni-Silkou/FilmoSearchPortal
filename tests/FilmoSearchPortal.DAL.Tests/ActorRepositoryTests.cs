using FilmoSearchPortal.DAL.Repositories;
using FilmoSearchPortal.DAL.Entities;

namespace FilmoSearchPortal.DAL.Tests;

public class ActorRepositoryTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActors()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ActorRepository(context);

        // Act
        var actors = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(actors);
        Assert.Equal(2, actors.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectActor()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ActorRepository(context);

        // Act
        var actor = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(actor);
        Assert.Equal("Robert", actor.FirstName);
        Assert.Equal("Downey Jr.", actor.LastName);
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewActor()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ActorRepository(context);
        var newActor = new Actor { Id = 3, FirstName = "Scarlett", LastName = "Johansson" };

        // Act
        await repository.AddAsync(newActor);
        var actor = await repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(actor);
        Assert.Equal("Scarlett", actor.FirstName);
        Assert.Equal("Johansson", actor.LastName);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingActor()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ActorRepository(context);
        var actorToUpdate = await repository.GetByIdAsync(1);
        actorToUpdate.LastName = "UpdatedName";

        // Act
        await repository.UpdateAsync(actorToUpdate);
        var updatedActor = await repository.GetByIdAsync(1);

        // Assert
        Assert.Equal("UpdatedName", updatedActor.LastName);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveActor()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ActorRepository(context);

        // Act
        await repository.DeleteAsync(1);
        var deletedActor = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(deletedActor);
    }
}