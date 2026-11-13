using TicketHub.Application.Events.AddEvent;

namespace TicketHub.Application.Events
{
    public interface IEventService
    {
        Task<int> AddEventAsync(AddEventCommand command, CancellationToken cancellationToken);
    }
}
