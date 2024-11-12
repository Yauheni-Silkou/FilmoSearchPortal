using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FilmoSearchPortal.WebAPI.Tests;

public class FilmsControllerTests
{
    private readonly Mock<IFilmService> _filmServiceMock;
    private readonly Mock<ILogger<FilmsController>> _loggerMock;
    private readonly FilmsController _controller;

    public FilmsControllerTests()
    {
        _filmServiceMock = new Mock<IFilmService>();
        _loggerMock = new Mock<ILogger<FilmsController>>();
        _controller = new FilmsController(_filmServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithFilms()
    {
        // Arrange
        var films = new List<FilmDto>
        {
            new() { Id = 1, Title = "Bruce Almighty", Actors = [new ActorDto { Id = 1, FirstName = "Jim", LastName = "Carrey" }] }
        };
        _filmServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(films);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFilms = Assert.IsType<List<FilmDto>>(okResult.Value);
        Assert.Single(returnedFilms);
        Assert.Equal("Bruce Almighty", returnedFilms[0].Title);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenFilmExists()
    {
        // Arrange
        var film = new FilmDto { Id = 1, Title = "Bruce Almighty", Actors = [new ActorDto { Id = 1, FirstName = "Jim", LastName = "Carrey" }] };
        _filmServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(film);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFilm = Assert.IsType<FilmDto>(okResult.Value);
        Assert.Equal("Bruce Almighty", returnedFilm.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenFilmDoesNotExist()
    {
        // Arrange
        _filmServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((FilmDto)null!);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenFilmIsCreated()
    {
        // Arrange
        var filmDto = new FilmDto { Id = 1, Title = "Bruce Almighty", Actors = [new() { Id = 1, FirstName = "Jim", LastName = "Carrey" }] };
        _filmServiceMock.Setup(service => service.AddAsync(filmDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(filmDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdAtActionResult.ActionName);
        Assert.Equal(1, createdAtActionResult?.RouteValues?["id"]);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenFilmIsUpdated()
    {
        // Arrange
        var filmDto = new FilmDto { Id = 1, Title = "Eternal Sunshine of the Spotless Mind", Actors = [new ActorDto { Id = 1, FirstName = "Jim", LastName = "Carrey" }] };
        _filmServiceMock.Setup(service => service.UpdateAsync(filmDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, filmDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var filmDto = new FilmDto { Id = 2, Title = "The Mask", Actors = [new ActorDto { Id = 2, FirstName = "Jim", LastName = "Carrey" }] };

        // Act
        var result = await _controller.Update(1, filmDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Film ID mismatch", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenFilmIsDeleted()
    {
        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}