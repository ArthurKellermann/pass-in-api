using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Infrastructure.Database;

namespace PassIn.Infrastructure.Repositories;
public class EventsRepository : IEventRepository
{
    private readonly PassInDbContext _dbContext;

    public EventsRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Event> GetEventById(Guid id)
    {
        var entity = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);

        return entity;
    }

    public Task<Event> Register(Event entity)
    {
        await _dbContext.Events.Add(entity);
        await _dbContext.SaveChangesAsync();
    }
}
