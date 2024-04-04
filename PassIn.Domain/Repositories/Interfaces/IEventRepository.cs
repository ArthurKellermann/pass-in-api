using PassIn.Domain.Entities;

namespace PassIn.Domain.Repositories.Interfaces;
public interface IEventRepository
{
    Task<Event> Register();
}
