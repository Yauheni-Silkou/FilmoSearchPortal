namespace FilmoSearchPortal.BLL.Services;

public class ActorService(IActorRepository actorRepository, IMapper mapper) : IActorService
{
    private readonly IActorRepository _actorRepository = actorRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IEnumerable<ActorDto>> GetAllAsync()
    {
        var actors = await _actorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ActorDto>>(actors);
    }

    public async Task<ActorDto> GetByIdAsync(int id)
    {
        var actor = await _actorRepository.GetByIdAsync(id);
        return actor is null ? null! : _mapper.Map<ActorDto>(actor);
    }

    public async Task AddAsync(ActorDto actorDto)
    {
        var actor = _mapper.Map<Actor>(actorDto);
        await _actorRepository.AddAsync(actor);
    }

    public async Task UpdateAsync(ActorDto actorDto)
    {
        var actor = _mapper.Map<Actor>(actorDto);
        await _actorRepository.UpdateAsync(actor);
    }

    public async Task DeleteAsync(int id)
    {
        await _actorRepository.DeleteAsync(id);
    }
}