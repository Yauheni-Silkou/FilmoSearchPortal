namespace FilmoSearchPortal.BLL.Services;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetAllAsync();

    Task<ReviewDto> GetByIdAsync(int id);

    Task AddAsync(ReviewDto reviewDto);

    Task UpdateAsync(ReviewDto reviewDto);

    Task DeleteAsync(int id);
}