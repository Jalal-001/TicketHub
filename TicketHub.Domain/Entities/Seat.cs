namespace TicketHub.Domain.Entities
{
    public class Seat : AuditableEntity
    {
        public required int EventId { get; set; }
        public required int Row { get; set; }
        public required int Number { get; set; }
        public required decimal Price { get; set; }
        public SeatStatus Status { get; set; }

        //navigation property for the related Event and OrderItems
        public Event Event { get; set; } = null!;
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public enum SeatStatus
    {
        Available,
        Reserved,
        Sold
    }
}
