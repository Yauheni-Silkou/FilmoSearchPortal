using AutoMapper;
using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entities;
using FilmoSearchPortal.DAL.Repositories;
using Moq;

namespace FilmoSearchPortal.BLL.Tests;

public class ActorServiceTests
{
    private readonly Mock<IActorRepository> _actorRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ActorService _actorService;

    public ActorServiceTests()
    {
        _actorRepositoryMock = new Mock<IActorRepository>();
        _mapperMock = new Mock<IMapper>();
        _actorService = new ActorService(_actorRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedActorDtos()
    {
        // Arrange
        var actors = new List<Actor> { new() { Id = 1, FirstName = "Sean", LastName = "Bean" } };
        _actorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(actors);
        _mapperMock.Setup(m => m.Map<IEnumerable<ActorDto>>(actors)).Returns(
        [
            new() { Id = 1, FirstName = "Sean", LastName = "Bean" }
        ]);

        // Act
        var result = await _actorService.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Sean", result.First().FirstName);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMappedActorDto_WhenActorExists()
    {
        // Arrange
        var actor = new Actor { Id = 1, FirstName = "Sean", LastName = "Bean" };
        _actorRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(actor);
        _mapperMock.Setup(m => m.Map<ActorDto>(actor)).Returns(new ActorDto { Id = 1, FirstName = "Sean", LastName = "Bean" });

        // Act
        var result = await _actorService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Sean", result.FirstName);
    }

    [Fact]
    public async Task AddAsync_ShouldMapAndSaveActor()
    {
        // Arrange
        var actorDto = new ActorDto { Id = 1, FirstName = "Bean", LastName = "Bean" };
        var actor = new Actor { Id = 1, FirstName = "Sean", LastName = "Bean" };
        _mapperMock.Setup(m => m.Map<Actor>(actorDto)).Returns(actor);

        // Act
        await _actorService.AddAsync(actorDto);

        // Assert
        _actorRepositoryMock.Verify(repo => repo.AddAsync(actor), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldMapAndUpdateActor()
    {
        // Arrange
        var actorDto = new ActorDto { Id = 1, FirstName = "Sean", LastName = "Bean" };
        var actor = new Actor { Id = 1, FirstName = "Sean", LastName = "Bean" };
        _mapperMock.Setup(m => m.Map<Actor>(actorDto)).Returns(actor);

        // Act
        await _actorService.UpdateAsync(actorDto);

        // Assert
        _actorRepositoryMock.Verify(repo => repo.UpdateAsync(actor), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDeleteOnRepository()
    {
        // Act
        await _actorService.DeleteAsync(1);

        // Assert
        _actorRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}