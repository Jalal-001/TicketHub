namespace TicketHub.Domain.Entities
{
    public class Order : AuditableEntity
    {
        public required string UserId { get; set; }
        public required decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        // Navigation property for the related OrderItems
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
    public enum OrderStatus
    {
        Pending,
        Completed,
        Cancelled
    }
}
