namespace FilmoSearchPortal.BLL.Services;

public class ReviewService(IReviewRepository reviewRepository, IMapper mapper) : IReviewService
{
    private readonly IReviewRepository _reviewRepository = reviewRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }

    public async Task<ReviewDto> GetByIdAsync(int id)
    {
        var review = await _reviewRepository.GetByIdAsync(id);
        return review is null ? null! : _mapper.Map<ReviewDto>(review);
    }

    public async Task AddAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        await _reviewRepository.AddAsync(review);
    }

    public async Task UpdateAsync(ReviewDto reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        await _reviewRepository.UpdateAsync(review);
    }

    public async Task DeleteAsync(int id)
    {
        await _reviewRepository.DeleteAsync(id);
    }
}