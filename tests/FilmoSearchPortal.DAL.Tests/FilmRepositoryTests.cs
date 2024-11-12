using FilmoSearchPortal.DAL.Entities;
using FilmoSearchPortal.DAL.Repositories;

namespace FilmoSearchPortal.DAL.Tests;

public class FilmRepositoryTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllFilms()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new FilmRepository(context);

        // Act
        var films = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(films);
        Assert.Equal(2, films.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectFilm()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new FilmRepository(context);

        // Act
        var film = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(film);
        Assert.Equal("Iron Man", film.Title);
        Assert.Single(film.Actors);
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewFilmWithExistingActor()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new FilmRepository(context);
        var existingActor = context.Actors.First();
        var newFilm = new Film { Id = 3, Title = "Avengers", Actors = [existingActor] };

        // Act
        await repository.AddAsync(newFilm);
        var film = await repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(film);
        Assert.Equal("Avengers", film.Title);
        Assert.Single(film.Actors);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingFilm()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new FilmRepository(context);
        var filmToUpdate = await repository.GetByIdAsync(1);
        filmToUpdate.Title = "Updated Title";

        // Act
        await repository.UpdateAsync(filmToUpdate);
        var updatedFilm = await repository.GetByIdAsync(1);

        // Assert
        Assert.Equal("Updated Title", updatedFilm.Title);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveFilm()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new FilmRepository(context);

        // Act
        await repository.DeleteAsync(1);
        var deletedFilm = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(deletedFilm);
    }
}