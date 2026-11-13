using TicketHub.Application.Events;

namespace TicketHub.Api.Endpoints.Events.AddEvent
{
    public class AddEventEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/events", Handle)
                .WithTags("Events");
        }

        private async Task<IResult> Handle(AddEventRequest request, IEventService eventService, CancellationToken cancellationToken)
        {
            var addEventCommand = request.ToAddEventCommand();

            var eventId = await eventService.AddEventAsync(addEventCommand, cancellationToken);
            return Results.Created($"/events/{eventId}", new AddEventResponse { EventId = eventId });
        }
    }
}
