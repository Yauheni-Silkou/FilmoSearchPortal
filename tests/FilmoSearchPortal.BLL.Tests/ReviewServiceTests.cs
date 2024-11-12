using AutoMapper;
using FilmoSearchPortal.BLL.Dtos;
using FilmoSearchPortal.BLL.Services;
using FilmoSearchPortal.DAL.Entities;
using FilmoSearchPortal.DAL.Repositories;
using Moq;

namespace FilmoSearchPortal.BLL.Tests;

public class ReviewServiceTests
{
    private readonly Mock<IReviewRepository> _reviewRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ReviewService _reviewService;

    public ReviewServiceTests()
    {
        _reviewRepositoryMock = new Mock<IReviewRepository>();
        _mapperMock = new Mock<IMapper>();
        _reviewService = new ReviewService(_reviewRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnMappedReviewDtos()
    {
        // Arrange
        var reviews = new List<Review>
        {
            new() { Id = 1, Title = "Great!", Stars = 5 },
            new() { Id = 2, Title = "Not bad", Stars = 3 }
        };
        _reviewRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(reviews);
        _mapperMock.Setup(m => m.Map<IEnumerable<ReviewDto>>(reviews)).Returns(
        [
            new ReviewDto { Id = 1, Title = "Great!", Stars = 5 },
            new ReviewDto { Id = 2, Title = "Not bad", Stars = 3 }
        ]);

        // Act
        var result = await _reviewService.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal("Great!", result.First().Title);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnMappedReviewDto_WhenReviewExists()
    {
        // Arrange
        var review = new Review { Id = 1, Title = "Great!", Stars = 5 };
        _reviewRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(review);
        _mapperMock.Setup(m => m.Map<ReviewDto>(review)).Returns(new ReviewDto { Id = 1, Title = "Great!", Stars = 5 });

        // Act
        var result = await _reviewService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Great!", result.Title);
    }

    [Fact]
    public async Task AddAsync_ShouldMapAndSaveReview()
    {
        // Arrange
        var reviewDto = new ReviewDto { Id = 1, Title = "Amazing!", Stars = 5 };
        var review = new Review { Id = 1, Title = "Amazing!", Stars = 5 };
        _mapperMock.Setup(m => m.Map<Review>(reviewDto)).Returns(review);

        // Act
        await _reviewService.AddAsync(reviewDto);

        // Assert
        _reviewRepositoryMock.Verify(repo => repo.AddAsync(review), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldMapAndUpdateReview()
    {
        // Arrange
        var reviewDto = new ReviewDto { Id = 1, Title = "Awesome!", Stars = 4 };
        var review = new Review { Id = 1, Title = "Awesome!", Stars = 4 };
        _mapperMock.Setup(m => m.Map<Review>(reviewDto)).Returns(review);

        // Act
        await _reviewService.UpdateAsync(reviewDto);

        // Assert
        _reviewRepositoryMock.Verify(repo => repo.UpdateAsync(review), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallDeleteOnRepository()
    {
        // Act
        await _reviewService.DeleteAsync(1);

        // Assert
        _reviewRepositoryMock.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }
}