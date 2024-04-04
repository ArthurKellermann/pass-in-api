using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Exceptions.CustomExceptions;
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
           .FirstOrDefault(ev => ev.Id == eventId);

        return entity.Attendees;
    }
    public async Task<Attendee> FindById(Guid id)
    {
        Attendee attendee = await _dbContext.Attendees.FirstOrDefaultAsync(attendee => attendee.Id == id);

        return attendee;
    }
}
