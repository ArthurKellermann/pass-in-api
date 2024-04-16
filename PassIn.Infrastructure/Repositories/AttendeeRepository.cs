using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Infrastructure.Database;

namespace PassIn.Domain.Repositories;
public class AttendeeRepository : IAttendeeRepository
{
    private readonly PassInDbContext _dbContext;
    public AttendeeRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Attendee>> GetAllByEventId(Guid eventId)
    {
        Event entity = await _dbContext.Events
           .Include(ev => ev.Attendees)
           .ThenInclude(attendee => attendee.CheckIn)
           .FirstOrDefaultAsync(ev => ev.Id == eventId);

        return entity.Attendees;
    }
    public async Task<Attendee> FindById(Guid id)
    {
        Attendee attendee = await _dbContext.Attendees.FirstOrDefaultAsync(attendee => attendee.Id == id);

        return attendee;
    }

    public async Task<Attendee> RegisterOnEvent(Guid eventId, Attendee attendee)
    {
        var entity = new Attendee
        {
            Name = attendee.Name,
            Email = attendee.Email,
            EventId = eventId,
            Created_At = DateTime.UtcNow,
        };

        _dbContext.Attendees.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> IsRegisteredForEvent(string email, Guid eventId)
    {
        var isRegistered = await _dbContext
            .Attendees
            .FirstOrDefaultAsync(att => att.Email.Equals(email) && att.EventId.Equals(eventId));

        if (isRegistered is null)
        {
            return false;
        }

        return true;
    }

}
