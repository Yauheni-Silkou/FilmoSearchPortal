namespace FilmoSearchPortal.DAL.Repositories;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();

    Task<Review> GetByIdAsync(int id);

    Task AddAsync(Review review);

    Task UpdateAsync(Review review);

    Task DeleteAsync(int id);
}