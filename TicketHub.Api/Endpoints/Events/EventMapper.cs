using TicketHub.Api.Endpoints.Events.AddEvent;
using TicketHub.Application.Events.AddEvent;

namespace TicketHub.Api.Endpoints.Events
{
    public static class EventMapper
    {
        public static AddEventCommand ToAddEventCommand(this AddEventRequest request) =>

            new()
            {
                Name = request.Name,
                Venue = request.Venue,
                StartsAt = request.StartsAt,
                Rows = request.Rows,
                SeatsPerRow = request.SeatsPerRow,
                Price = request.Price
            };
    }
}
