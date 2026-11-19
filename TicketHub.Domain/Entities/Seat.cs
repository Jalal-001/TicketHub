using TicketHub.Domain.Exceptions;

namespace TicketHub.Domain.Entities
{
    public class Seat : AuditableEntity
    {
        private Seat()
        {

        }
        internal Seat(int row, int number, decimal price)
        {
            Row = row;
            Number = number;
            Price = price;
        }

        public int EventId { get; private set; }
        public int Row { get; private set; }
        public int Number { get; private set; }
        public decimal Price { get; private set; }
        public SeatStatus Status { get; private set; }

        //navigation property for the related Event and OrderItems
        public Event Event { get; set; } = null!;
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public void Release()
        {
            if (Status != SeatStatus.Reserved)
            {
                throw new DomainException("SeatNotReserved", $"Seat {Row}-{Number} is not reserved.");
            }
            Status = SeatStatus.Available;
        }

        public void Reserve()
        {
            if (Status != SeatStatus.Available)
            {
                throw new DomainException("SeatNotAvailable", $"Seat {Row}-{Number} is not available.");
            }
            Status = SeatStatus.Reserved;
        }

        public void Sell()
        {
            if (Status != SeatStatus.Reserved)
            {
                throw new DomainException("SeatNotReserved", $"Seat {Row}-{Number} is not reserved.");
            }
            Status = SeatStatus.Sold;
        }
    }

    public enum SeatStatus
    {
        Available,
        Reserved,
        Sold
    }
}
