using TicketHub.Application.Abstractions;
using TicketHub.Application.Events.GetEvents;
using TicketHub.Application.Events.GetEventSeats;
using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IReadOnlyList<EventSummaryDto>> GetEvents(CancellationToken cancellationToken);
        Task<EventSeatsDto?> GetEventSeatsAsync(int id, SeatStatus? status, CancellationToken cancellationToken);
    }
}
