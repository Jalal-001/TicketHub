using TicketHub.Application.Events;
using TicketHub.Domain.Entities;

namespace TicketHub.Api.Endpoints.Events.GetEventSeats
{
    public class GetEventSeatsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/events/{id:int}/seats", Handle)
                .WithTags("Events");
        }

        public async Task<IResult> Handle(int id, SeatStatus? status, IEventService eventService, CancellationToken cancellationToken)
        {
            var eventSeats = await eventService.GetEventSeatsAsync(id, status, cancellationToken);

            return Results.Ok(eventSeats.ToGetEventSeatsResponse());
        }
    }
}
