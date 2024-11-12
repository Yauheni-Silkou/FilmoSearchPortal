using AutoMapper;
using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entities;
using FilmoSearchPortal.DAL.Repositories;
using Moq;

namespace FilmoSearchPortal.BLL.Tests;

public class FilmServiceTests
{
    private readonly Mock<IFilmRepository> _filmRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly FilmService _filmService;

    public FilmServiceTests()
    {
        _filmRepositoryMock = new Mock<IFilmRepository>();
        _mapperMock = new Mock<IMapper>();
        _filmService = new FilmService(_filmRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedFilmDtos()
    {
        // Arrange
        var films = new List<Film> { new() { Id = 1, Title = "Green Book" } };
        _filmRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(films);
        _mapperMock.Setup(m => m.Map<IEnumerable<FilmDto>>(films)).Returns(
        [
            new FilmDto { Id = 1, Title = "Green Book" }
        ]);

        // Act
        var result = await _filmService.GetAllAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Green Book", result.First().Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMappedFilmDto_WhenFilmExists()
    {
        // Arrange
        var film = new Film { Id = 1, Title = "Green Book" };
        _filmRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(film);
        _mapperMock.Setup(m => m.Map<FilmDto>(film)).Returns(new FilmDto { Id = 1, Title = "Green Book" });

        // Act
        var result = await _filmService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Green Book", result.Title);
    }

    [Fact]
    public async Task AddAsync_ShouldMapAndSaveFilm()
    {
        // Arrange
        var filmDto = new FilmDto { Id = 1, Title = "Green Book" };
        var film = new Film { Id = 1, Title = "Green Book" };
        _mapperMock.Setup(m => m.Map<Film>(filmDto)).Returns(film);

        // Act
        await _filmService.AddAsync(filmDto);

        // Assert
        _filmRepositoryMock.Verify(repo => repo.AddAsync(film), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldMapAndUpdateFilm()
    {
        // Arrange
        var filmDto = new FilmDto { Id = 1, Title = "The Blue Book" };
        var film = new Film { Id = 1, Title = "The Blue Book" };
        _mapperMock.Setup(m => m.Map<Film>(filmDto)).Returns(film);

        // Act
        await _filmService.UpdateAsync(filmDto);

        // Assert
        _filmRepositoryMock.Verify(repo => repo.UpdateAsync(film), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDeleteOnRepository()
    {
        // Act
        await _filmService.DeleteAsync(1);

        // Assert
        _filmRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}