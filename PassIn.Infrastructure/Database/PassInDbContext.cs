using Microsoft.EntityFrameworkCore;
using PassIn.Domain.Entities;

namespace PassIn.Infrastructure.Database;
public class PassInDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<CheckIn> CheckIns { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=\"D:\\OneDrive\\Documentos\\C#\\PassIn\\PassInDb.db\"");
    }
}
