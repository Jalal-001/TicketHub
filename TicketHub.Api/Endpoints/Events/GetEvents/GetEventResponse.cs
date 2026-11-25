namespace TicketHub.Api.Endpoints.Events.GetEvents
{
    public record GetEventResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Venue { get; init; }
        public required DateTime StartsAt { get; init; }
        public required int TotalSeats { get; init; }
        public required int AvailableSeats { get; init; }
    }
}
