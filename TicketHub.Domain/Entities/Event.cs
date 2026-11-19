using TicketHub.Domain.Exceptions;

namespace TicketHub.Domain.Entities
{
    public class Event : AuditableEntity
    {
        private readonly List<Seat> _seats = new();
        private Event()
        {

        }
        public Event(string name, string venue, DateTime startsAt)
        {
            Name = name;
            Venue = venue;
            StartsAt = startsAt;
        }

        public string Name { get; private set; }
        public string Venue { get; private set; }
        public DateTime StartsAt { get; private set; }

        //Navigation property for Seats
        public IReadOnlyCollection<Seat> Seats => _seats;

        public void AddSeats(int rows, int seatsPerRow, decimal price)
        {
            if (_seats.Any())
            {
                throw new DomainException("SeatsAlreadyAdded", "Seats have already been added to this event.");
            }
            if (rows <= 0 || seatsPerRow <= 0)
            {
                throw new DomainException("InvalidSeatInput", "Rows and seats per row must be greater than zero.");
            }
            if (price < 0)
            {
                throw new DomainException("InvalidSeatInput", "Seat price cannot be negative.");
            }

            for (int row = 1; row <= rows; row++)
            {
                for (int column = 1; column <= seatsPerRow; column++)
                {
                    _seats.Add(new Seat(row, column, price));
                }
            }
        }
    }
}
