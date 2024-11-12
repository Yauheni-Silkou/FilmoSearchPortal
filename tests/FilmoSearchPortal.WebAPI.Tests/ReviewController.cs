#pragma warning disable IDE0090

using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.WebAPI.Controllers;

namespace FilmoSearchPortal.WebAPI.Tests;
public class ReviewsControllerTests
{
    private readonly Mock<IReviewService> _reviewServiceMock;
    private readonly Mock<ILogger<ReviewsController>> _loggerMock;
    private readonly ReviewsController _controller;

    public ReviewsControllerTests()
    {
        _reviewServiceMock = new Mock<IReviewService>();
        _loggerMock = new Mock<ILogger<ReviewsController>>();
        _controller = new ReviewsController(_reviewServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithReviews()
    {
        // Arrange
        var reviews = new List<ReviewDto>
    {
        new ReviewDto
        {
            Id = 1,
            Title = "Great Movie",
            Description = "A thrilling experience",
            Stars = 5,
            Film = new FilmDto
            {
                Id = 1,
                Title = "Inception",
                Actors =
                [
                    new ActorDto { Id = 1, FirstName = "Leonardo", LastName = "DiCaprio" }
                ]
            }
        }
    };
        _reviewServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(reviews);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedReviews = Assert.IsType<List<ReviewDto>>(okResult.Value);
        Assert.Single(returnedReviews);
        Assert.Equal("Great Movie", returnedReviews[0].Title);
        Assert.Equal("Inception", returnedReviews[0].Film.Title);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenReviewExists()
    {
        // Arrange
        var review = new ReviewDto
        {
            Id = 1,
            Title = "Amazing Film",
            Description = "Mind-blowing story",
            Stars = 5,
            Film = new FilmDto
            {
                Id = 1,
                Title = "Interstellar",
                Actors =
                [
                    new ActorDto { Id = 1, FirstName = "Matthew", LastName = "McConaughey" }
                ]
            }
        };
        _reviewServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(review);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedReview = Assert.IsType<ReviewDto>(okResult.Value);
        Assert.Equal("Amazing Film", returnedReview.Title);
        Assert.Equal("Interstellar", returnedReview.Film.Title);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenReviewDoesNotExist()
    {
        // Arrange
        _reviewServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync((ReviewDto)null!);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenReviewIsCreated()
    {
        // Arrange
        var reviewDto = new ReviewDto
        {
            Id = 1,
            Title = "Great Film",
            Description = "Loved every moment",
            Stars = 4,
            Film = new FilmDto
            {
                Id = 2,
                Title = "The Dark Knight",
                Actors =
                [
                    new ActorDto { Id = 1, FirstName = "Christian", LastName = "Bale" }
                ]
            }
        };
        _reviewServiceMock.Setup(service => service.AddAsync(reviewDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(reviewDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetById", createdAtActionResult.ActionName);
        Assert.Equal(1, createdAtActionResult?.RouteValues?["id"]);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenReviewIsUpdated()
    {
        // Arrange
        var reviewDto = new ReviewDto
        {
            Id = 1,
            Title = "Updated Review",
            Description = "Even better after rewatching",
            Stars = 5,
            Film = new FilmDto
            {
                Id = 3,
                Title = "The Prestige",
                Actors =
                [
                    new ActorDto { Id = 2, FirstName = "Hugh", LastName = "Jackman" }
                ]
            }
        };
        _reviewServiceMock.Setup(service => service.UpdateAsync(reviewDto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Update(1, reviewDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var reviewDto = new ReviewDto
        {
            Id = 2,
            Title = "Mismatch Review",
            Description = "Description mismatch",
            Stars = 2,
            Film = new FilmDto { Id = 1, Title = "Film Title Mismatch" }
        };

        // Act
        var result = await _controller.Update(1, reviewDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Review ID mismatch", badRequestResult.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenReviewIsDeleted()
    {
        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}