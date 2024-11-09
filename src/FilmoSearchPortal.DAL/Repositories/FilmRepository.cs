namespace FilmoSearchPortal.DAL.Repositories;

public class FilmRepository : IFilmRepository
{
    public Task<IEnumerable<Film>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Film> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Film film)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Film film)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}