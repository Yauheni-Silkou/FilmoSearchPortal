using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FilmoSearchPortal.WebAPI.Tests;

public class ActorsControllerTests
{
    private readonly Mock<IActorService> _actorServiceMock;
    private readonly Mock<ILogger<ActorsController>> _loggerMock;
    private readonly ActorsController _controller;

    public ActorsControllerTests()
    {
        _actorServiceMock = new Mock<IActorService>();
        _loggerMock = new Mock<ILogger<ActorsController>>();
        _controller = new ActorsController(_actorServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithActors()
    {
        // Arrange
        var actors = new List<ActorDto>
        {
            new() { Id = 1, FirstName = "Keanu", LastName = "Reeves" }
        };
        _actorServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(actors);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedActors = Assert.IsType<List<ActorDto>>(okResult.Value);
        Assert.Single(returnedActors);
        Assert.Equal("Keanu", returnedActors[0].FirstName);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenActorExists()
    {
        // Arrange
        var actor = new ActorDto { Id = 1, FirstName = "Keanu", LastName = "Reeves" };
        _actorServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(actor);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedActor = Assert.IsType<ActorDto>(okResult.Value);
        Assert.Equal("Keanu", returnedActor.FirstName);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenActorDoesNotExist()
    {
        // Arrange
        _actorServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ActorDto)null!);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenActorIsCreated()
    {
        // Arrange
        var actorDto = new ActorDto { Id = 1, FirstName = "Keanu", LastName = "Reeves" };
        _actorServiceMock.Setup(service => service.AddAsync(actorDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(actorDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdAtActionResult.ActionName);
        Assert.Equal(1, createdAtActionResult?.RouteValues?["id"]);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenActorIsUpdated()
    {
        // Arrange
        var actorDto = new ActorDto { Id = 1, FirstName = "Keanu", LastName = "Reeves" };
        _actorServiceMock.Setup(service => service.UpdateAsync(actorDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, actorDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var actorDto = new ActorDto { Id = 2, FirstName = "Keanu", LastName = "Reeves" };

        // Act
        var result = await _controller.Update(1, actorDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Actor ID mismatch", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenActorIsDeleted()
    {
        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}