namespace FilmoSearchPortal.DAL.Repositories;

public class ActorRepository(ApplicationDbContext context) : IActorRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        var actors = await _context.Actors.ToListAsync();
        return actors;
    }

    public async Task<Actor> GetByIdAsync(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        return actor!;
    }

    public async Task AddAsync(Actor actor)
    {
        await _context.Actors.AddAsync(actor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Actor actor)
    {
        var existingActor = await _context.Actors.FindAsync(actor.Id);
        if (existingActor is not null)
        {
            _context.Entry(existingActor).CurrentValues.SetValues(actor);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var actor = await _context.Actors.FindAsync(id);
        if (actor is not null)
        {
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
        }
    }
}