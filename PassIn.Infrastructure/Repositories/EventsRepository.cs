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

    public async Task<Event> Register(Event entity)
    {
        await _dbContext.Events.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }
    public async Task<int> GetNumberOfAttendeesByEventId(Guid id)
    {
        var numberOfAttendessForEvenet = await _dbContext.Attendees.CountAsync(attendee => attendee.EventId == id);

        return numberOfAttendessForEvenet;
    }
}
