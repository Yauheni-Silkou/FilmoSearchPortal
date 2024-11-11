namespace FilmoSearchPortal.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController(IReviewService reviewService, ILogger<ReviewsController> logger) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;
    private readonly ILogger<ReviewsController> _logger = logger;

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
    public async Task<IActionResult> Create(ReviewDto review)
    {
        if (review is null)
        {
            return BadRequest("Review data is required");
        }

        await _reviewService.AddAsync(review);
        return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ReviewDto review)
    {
        if (id != review.Id)
        {
            return BadRequest("Review ID mismatch");
        }

        await _reviewService.UpdateAsync(review);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.DeleteAsync(id);
        return NoContent();
    }
}