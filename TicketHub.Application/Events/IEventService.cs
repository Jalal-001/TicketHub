using TicketHub.Application.Events.AddEvent;
using TicketHub.Application.Events.GetEvents;
using TicketHub.Application.Events.GetEventSeats;
using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events
{
    public interface IEventService
    {
        Task<int> AddEventAsync(AddEventCommand command, CancellationToken cancellationToken);
        Task<IReadOnlyList<EventSummaryDto>> GetEventsAsync(CancellationToken cancellationToken);
        Task<EventSeatsDto> GetEventSeatsAsync(int id, SeatStatus? status, CancellationToken cancellationToken);
    }
}
