namespace TicketHub.Domain.Entities
{
    public class Event : AuditableEntity
    {
        public required string Name { get; set; }
        public required string Venue { get; set; }
        public required DateTime StartsAt { get; set; }
        //Navigation property for Seats
        public IList<Seat> Seats { get; set; } = new List<Seat>();
    }
}
