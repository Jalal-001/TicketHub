namespace TicketHub.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        public required int OrderId { get; set; }
        public required int SeatId { get; set; }
        public required decimal Price { get; set; }

        // Navigation property for the related Order and Seat
        public Order Order { get; set; } = null!;
        public Seat Seat { get; set; } = null!;
    }
}
