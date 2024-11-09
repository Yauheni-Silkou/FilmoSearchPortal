namespace FilmoSearchPortal.BLL.Services;

public interface IActorService
{
    Task<IEnumerable<ActorDto>> GetAllAsync();

    Task<ActorDto> GetByIdAsync(int id);

    Task AddAsync(ActorDto actorDto);

    Task UpdateAsync(ActorDto actorDto);

    Task DeleteAsync(int id);
}