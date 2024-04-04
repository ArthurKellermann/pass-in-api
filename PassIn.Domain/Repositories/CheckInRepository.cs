using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;
using PassIn.Domain.Repositories.Interfaces;
using PassIn.Infrastructure.Database;

namespace PassIn.Domain.Repositories;
public class CheckInRepository : ICheckInRepository
{
    private readonly PassInDbContext _dbContext;

    public CheckInRepository(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CheckIn> CheckInAttendee(Guid attendeeId)
    {
        var checkIn = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_at = DateTime.UtcNow,
        };

        await _dbContext.CheckIns.AddAsync(checkIn);
        await _dbContext.SaveChangesAsync(); 

        return checkIn;
    }


    public async Task<CheckIn> FindByAttendeeId(Guid attendeeId)
    {
        CheckIn checkIn = await _dbContext.CheckIns.FirstOrDefaultAsync(checkIn => checkIn.Attendee_Id == attendeeId);

        return checkIn;
    }

}
