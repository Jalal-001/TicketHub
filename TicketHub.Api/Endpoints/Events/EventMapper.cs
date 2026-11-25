using TicketHub.Api.Endpoints.Events.AddEvent;
using TicketHub.Api.Endpoints.Events.GetEvents;
using TicketHub.Api.Endpoints.Events.GetEventSeats;
using TicketHub.Application.Events.AddEvent;
using TicketHub.Application.Events.GetEvents;
using TicketHub.Application.Events.GetEventSeats;

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

        public static IReadOnlyList<GetEventResponse> ToGetEventResponse(this IReadOnlyList<EventSummaryDto> getListOfEventDto) =>

            getListOfEventDto.Select(e => new GetEventResponse()
            {
                Id = e.Id,
                Name = e.Name,
                Venue = e.Venue,
                StartsAt = e.StartsAt,
                TotalSeats = e.TotalSeats,
                AvailableSeats = e.AvailableSeats
            }).ToList();

        public static GetEventSeatsResponse ToGetEventSeatsResponse(this EventSeatsDto getEventSeatsQuery) =>

            new()
            {
                Id = getEventSeatsQuery.Id,
                Name = getEventSeatsQuery.Name,
                Venue = getEventSeatsQuery.Venue,
                StartsAt = getEventSeatsQuery.StartsAt,
                Seats = getEventSeatsQuery.Seats.Select(s => new SeatResponse
                {
                    Row = s.Row,
                    Number = s.Number,
                    Price = s.Price,
                    Status = s.Status

                }).ToList()
            };
    }
}
