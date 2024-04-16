using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface IEventRepository
{
    Task<Event> Register(Event entity);
    Task<Event> GetEventById(Guid id);
}
