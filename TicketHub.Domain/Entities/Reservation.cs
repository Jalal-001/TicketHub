namespace TicketHub.Domain.Entities
{
    public class Reservation : AuditableEntity
    {
        public required string UserId { get; set; }
        public required int SeatId { get; set; }
        public required DateTime ExpiresAt { get; set; }
        public ReservationStatus Status { get; set; }

        //Navigation property for Seat
        public Seat Seat { get; set; } = null!;

    }
    public enum ReservationStatus
    {
        Active,
        Confirmed,
        Expired,
        Cancelled
    }
}
