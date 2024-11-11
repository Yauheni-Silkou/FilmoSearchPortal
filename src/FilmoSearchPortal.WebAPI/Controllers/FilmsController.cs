namespace FilmoSearchPortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilmsController(IFilmService filmService, ILogger<FilmsController> logger) : ControllerBase
{
    private readonly IFilmService _filmService = filmService;
    private readonly ILogger<FilmsController> _logger = logger;

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
    public async Task<IActionResult> Create(FilmDto film)
    {
        if (film is null)
        {
            return BadRequest("Film data is required");
        }

        await _filmService.AddAsync(film);
        return CreatedAtAction(nameof(GetById), new { id = film.Id }, film);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, FilmDto film)
    {
        if (id != film.Id)
        {
            return BadRequest("Film ID mismatch");
        }

        await _filmService.UpdateAsync(film);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _filmService.DeleteAsync(id);
        return NoContent();
    }
}