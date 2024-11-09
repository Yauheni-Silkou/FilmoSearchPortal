namespace FilmoSearchPortal.DAL.Repositories;

public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var reviews = await _context.Reviews.ToListAsync();
        return reviews;
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        return review!;
    }

    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        var existingReview = await _context.Reviews.FindAsync(review.Id);
        if (existingReview is not null)
        {
            _context.Entry(existingReview).CurrentValues.SetValues(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var film = await _context.Reviews.FindAsync(id);
        if (film is not null)
        {
            _context.Reviews.Remove(film);
            await _context.SaveChangesAsync();
        }
    }
}