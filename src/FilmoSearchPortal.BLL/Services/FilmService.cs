namespace FilmoSearchPortal.BLL.Services;

public class FilmService(IFilmRepository filmRepository, IMapper mapper) : IFilmService
{
    private readonly IFilmRepository _filmRepository = filmRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<FilmDto>> GetAllAsync()
    {
        var films = await _filmRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FilmDto>>(films);
    }

    public async Task<FilmDto> GetByIdAsync(int id)
    {
        var film = await _filmRepository.GetByIdAsync(id);
        return film is null ? null! : _mapper.Map<FilmDto>(film);
    }

    public async Task AddAsync(FilmDto filmDto)
    {
        var film = _mapper.Map<Film>(filmDto);
        await _filmRepository.AddAsync(film);
    }

    public async Task UpdateAsync(FilmDto filmDto)
    {
        var film = _mapper.Map<Film>(filmDto);
        await _filmRepository.UpdateAsync(film);
    }

    public async Task DeleteAsync(int id)
    {
        await _filmRepository.DeleteAsync(id);
    }
}