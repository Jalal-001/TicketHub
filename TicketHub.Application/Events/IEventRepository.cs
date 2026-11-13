using TicketHub.Application.Abstractions;
using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events
{
    public interface IEventRepository : IRepository<Event>
    {
    }
}
