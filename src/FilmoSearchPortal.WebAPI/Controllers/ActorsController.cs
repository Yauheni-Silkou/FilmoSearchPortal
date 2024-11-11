namespace FilmoSearchPortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorsController(IActorService actorService, ILogger<ActorsController> logger) : ControllerBase
{
    private readonly IActorService _actorService = actorService;
    private readonly ILogger<ActorsController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var actors = await _actorService.GetAllAsync();
        return Ok(actors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var actor = await _actorService.GetByIdAsync(id);
        return actor is not null ? Ok(actor) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActorDto actor)
    {
        if (actor is null)
        {
            return BadRequest("Actor data is required");
        }

        await _actorService.AddAsync(actor);
        return CreatedAtAction(nameof(GetById), new { id = actor.Id }, actor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActorDto actor)
    {
        if (id != actor.Id)
        {
            return BadRequest("Actor ID mismatch");
        }

        await _actorService.UpdateAsync(actor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _actorService.DeleteAsync(id);
        return NoContent();
    }
}