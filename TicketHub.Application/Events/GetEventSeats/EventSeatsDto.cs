using TicketHub.Domain.Entities;

namespace TicketHub.Application.Events.GetEventSeats
{
    public record EventSeatsDto
    {
        public required int Id { get; init; }
        public required string Name { get; init; }
        public required string Venue { get; init; }
        public required DateTime StartsAt { get; init; }
        public IList<SeatDto> Seats { get; init; } = new List<SeatDto>();
    }

    public record SeatDto
    {
        public required int Row { get; init; }
        public required int Number { get; init; }
        public required decimal Price { get; init; }
        public SeatStatus Status { get; init; }
    }
}
