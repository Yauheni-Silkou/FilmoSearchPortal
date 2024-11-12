namespace FilmoSearchPortal.DAL.Repositories;

public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var reviews = await _context.Reviews
            .Include(r => r.Film)
            .ToListAsync();

        return reviews;
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        var review = await _context.Reviews
            .Include(r => r.Film)
            .FirstOrDefaultAsync(r => r.Id == id);

        return review!;
    }

    public async Task AddAsync(Review review)
    {
        var existingFilm = await _context.Films
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == review.Film.Id);

        var trackedFilm = _context.Films.Local.FirstOrDefault(f => f.Id == review.FilmId);
        if (trackedFilm is not null)
        {
            review.Film = trackedFilm;
        }
        else if (existingFilm is not null)
        {
            _context.Attach(existingFilm);
            review.Film = existingFilm;
        }

        await _context.Reviews.AddAsync(review);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review review)
    {
        var existingReview = await _context.Reviews
            .Include(r => r.Film)
            .FirstOrDefaultAsync(r => r.Id == review.Id);

        if (existingReview is not null)
        {
            var trackedFilm = _context.Films.Local.FirstOrDefault(f => f.Id == review.FilmId);
            if (trackedFilm is not null)
            {
                review.Film = trackedFilm;
            }
            else
            {
                var existingFilm = await _context.Films
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == review.Film.Id);

                if (existingFilm is not null)
                {
                    _context.Attach(existingFilm);
                    review.Film = existingFilm;
                }
            }

            _context.Entry(existingReview).CurrentValues.SetValues(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var film = await _context.Reviews
            .FirstOrDefaultAsync(r => r.Id == id);

        if (film is not null)
        {
            _context.Reviews.Remove(film);
            await _context.SaveChangesAsync();
        }
    }
}