namespace FilmoSearchPortal.DAL.Repositories;

public class ActorRepository : IActorRepository
{
    public Task<IEnumerable<Actor>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Actor> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Actor actor)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Actor actor)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}