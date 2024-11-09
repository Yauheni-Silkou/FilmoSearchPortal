namespace FilmoSearchPortal.DAL.Repositories;

public class FilmRepository(ApplicationDbContext context) : IFilmRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Film>> GetAllAsync()
    {
        var films = await _context.Films.ToListAsync();
        return films;
    }

    public async Task<Film> GetByIdAsync(int id)
    {
        var film = await _context.Films.FindAsync(id);
        return film!;
    }

    public async Task AddAsync(Film film)
    {
        await _context.Films.AddAsync(film);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Film film)
    {
        var existingFilm = await _context.Films.FindAsync(film.Id);
        if (existingFilm is not null)
        {
            _context.Entry(existingFilm).CurrentValues.SetValues(film);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film is not null)
        {
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
        }
    }
}