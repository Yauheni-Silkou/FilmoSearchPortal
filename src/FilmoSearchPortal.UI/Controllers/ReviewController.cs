namespace FilmoSearchPortal.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        return review is not null ? Ok(review) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ReviewDto reviewDto)
    {
        if (reviewDto is null)
        {
            return BadRequest("Review data is required");
        }

        await _reviewService.AddAsync(reviewDto);
        return CreatedAtAction(nameof(GetById), new { id = reviewDto.Id }, reviewDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReviewDto reviewDto)
    {
        if (id != reviewDto.Id)
        {
            return BadRequest("Review ID mismatch");
        }

        await _reviewService.UpdateAsync(reviewDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.DeleteAsync(id);
        return NoContent();
    }
}