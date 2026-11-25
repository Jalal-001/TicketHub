using TicketHub.Application.Events;

namespace TicketHub.Api.Endpoints.Events.GetEvents
{
    public class GetEventsEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/events", Handle)
                .WithTags("Events");
        }

        private async Task<IResult> Handle(IEventService eventService, CancellationToken cancellation)
        {
            var events = await eventService.GetEventsAsync(cancellation);

            return Results.Ok(events.ToGetEventResponse());
        }
    }
}
