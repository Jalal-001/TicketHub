using TicketHub.Domain.Entities;

namespace TicketHub.Api.Endpoints.Events.GetEventSeats
{
    public record GetEventSeatsResponse
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Venue { get; init; }
        public required DateTime StartsAt { get; init; }
        public IList<SeatResponse> Seats { get; init; } = new List<SeatResponse>();
    }

    public record SeatResponse
    {
        public required int Row { get; init; }
        public required int Number { get; init; }
        public required decimal Price { get; init; }
        public SeatStatus Status { get; init; }
    }
}
