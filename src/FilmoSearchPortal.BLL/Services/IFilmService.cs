namespace FilmoSearchPortal.BLL.Services;

public interface IFilmService
{
    Task<IEnumerable<FilmDto>> GetAllAsync();

    Task<FilmDto> GetByIdAsync(int id);

    Task AddAsync(FilmDto filmDto);

    Task UpdateAsync(FilmDto filmDto);

    Task DeleteAsync(int id);
}