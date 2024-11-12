namespace FilmoSearchPortal.DAL.Repositories;

public class FilmRepository(ApplicationDbContext context) : IFilmRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Film>> GetAllAsync()
    {
        var films = await _context.Films
            .Include(f => f.Actors)
            .ToListAsync();

        return films;
    }

    public async Task<Film> GetByIdAsync(int id)
    {
        var film = await _context.Films
            .Include(f => f.Actors)
            .FirstOrDefaultAsync(f => f.Id == id);

        return film!;
    }

    public async Task AddAsync(Film film)
    {
        var newActors = new List<Actor>();

        foreach (var actor in film.Actors)
        {
            var existingActor = await _context.Actors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == actor.Id);

            if (existingActor is not null)
            {
                _context.Attach(existingActor);
                newActors.Add(existingActor);
            }
            else
            {
                newActors.Add(actor);
            }
        }

        film.Actors.Clear();
        film.Actors.AddRange(newActors);

        await _context.Films.AddAsync(film);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Film film)
    {
        var existingFilm = await _context.Films
            .Include(f => f.Actors)
            .FirstOrDefaultAsync(f => f.Id == film.Id);

        if (existingFilm is not null)
        {
            var newActors = new List<Actor>();

            foreach (var actor in film.Actors)
            {
                var trackedActor = _context.Actors.Local.FirstOrDefault(a => a.Id == actor.Id);

                if (trackedActor is not null)
                {
                    newActors.Add(trackedActor);
                }
                else
                {
                    var existingActor = await _context.Actors
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Id == actor.Id);

                    if (existingActor is not null)
                    {
                        _context.Attach(existingActor);
                        newActors.Add(existingActor);
                    }
                    else
                    {
                        newActors.Add(actor);
                    }
                }
            }

            existingFilm.Actors.Clear();
            existingFilm.Actors.AddRange(newActors);

            _context.Entry(existingFilm).CurrentValues.SetValues(film);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var film = await _context.Films
            .FirstOrDefaultAsync(f => f.Id == id);

        if (film is not null)
        {
            _context.Films.Remove(film);
            await _context.SaveChangesAsync();
        }
    }
}