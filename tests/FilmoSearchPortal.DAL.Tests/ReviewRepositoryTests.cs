using FilmoSearchPortal.DAL.Repositories;
using FilmoSearchPortal.DAL.Entities;

namespace FilmoSearchPortal.DAL.Tests;

public class ReviewRepositoryTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllReviews()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ReviewRepository(context);

        // Act
        var reviews = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(reviews);
        Assert.Equal(2, reviews.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCorrectReview()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ReviewRepository(context);

        // Act
        var review = await repository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(review);
        Assert.Equal("Amazing!", review.Title);
        Assert.Equal(5, review.Stars);
        Assert.NotNull(review.Film);
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewReviewForExistingFilm()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ReviewRepository(context);
        var existingFilm = context.Films.First();
        var newReview = new Review
        {
            Id = 3,
            Title = "Fantastic!",
            Description = "Exceeded expectations.",
            Stars = 5,
            FilmId = existingFilm.Id,
            Film = existingFilm
        };

        // Act
        await repository.AddAsync(newReview);
        var review = await repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(review);
        Assert.Equal("Fantastic!", review.Title);
        Assert.Equal(5, review.Stars);
        Assert.Equal(existingFilm.Id, review.FilmId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingReview()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ReviewRepository(context);
        var reviewToUpdate = await repository.GetByIdAsync(1);
        reviewToUpdate.Stars = 3;

        // Act
        await repository.UpdateAsync(reviewToUpdate);
        var updatedReview = await repository.GetByIdAsync(1);

        // Assert
        Assert.Equal(3, updatedReview.Stars);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveReview()
    {
        // Arrange
        using var context = GetContextWithData();
        var repository = new ReviewRepository(context);

        // Act
        await repository.DeleteAsync(1);
        var deletedReview = await repository.GetByIdAsync(1);

        // Assert
        Assert.Null(deletedReview);
    }
}