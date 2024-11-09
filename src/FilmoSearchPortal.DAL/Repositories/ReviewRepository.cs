namespace FilmoSearchPortal.DAL.Repositories;

public class ReviewRepository : IReviewRepository
{
    public Task<IEnumerable<Review>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Review> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}