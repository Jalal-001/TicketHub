namespace TicketHub.Api.Endpoints.Events.AddEvent
{
    public record AddEventRequest
    {
        public required string Name { get; init; }
        public required string Venue { get; init; }
        public required DateTime StartsAt { get; init; }
        public required int Rows { get; init; }
        public required int SeatsPerRow { get; init; }
        public required decimal Price { get; init; }
    }
}
