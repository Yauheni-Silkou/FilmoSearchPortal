namespace FilmoSearchPortal.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmController(IFilmService filmService, ILogger<FilmController> logger) : ControllerBase
{
    private readonly IFilmService _filmService = filmService;
    private readonly ILogger<FilmController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var films = await _filmService.GetAllAsync();
        return Ok(films);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var film = await _filmService.GetByIdAsync(id);
        return film is not null ? Ok(film) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(FilmDto filmDto)
    {
        if (filmDto is null)
        {
            return BadRequest("Film data is required");
        }

        await _filmService.AddAsync(filmDto);
        return CreatedAtAction(nameof(GetById), new { id = filmDto.Id }, filmDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FilmDto filmDto)
    {
        if (id != filmDto.Id)
        {
            return BadRequest("Film ID mismatch");
        }

        await _filmService.UpdateAsync(filmDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _filmService.DeleteAsync(id);
        return NoContent();
    }
}