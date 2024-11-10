namespace FilmoSearchPortal.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorController(IActorService actorService) : ControllerBase
{
    private readonly IActorService _actorService = actorService;

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
    public async Task<IActionResult> Create(ActorDto actorDto)
    {
        if (actorDto is null)
        {
            return BadRequest("Actor data is required");
        }

        await _actorService.AddAsync(actorDto);
        return CreatedAtAction(nameof(GetById), new { id = actorDto.Id }, actorDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActorDto actorDto)
    {
        if (id != actorDto.Id)
        {
            return BadRequest("Actor ID mismatch");
        }

        await _actorService.UpdateAsync(actorDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _actorService.DeleteAsync(id);
        return NoContent();
    }
}