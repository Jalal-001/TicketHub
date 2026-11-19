using TicketHub.Domain.Entities;
using TicketHub.Domain.Exceptions;

namespace TicketHub.Tests.Seats
{
    public class SeatTests
    {
        [Theory]
        [InlineData(5, 10, 19.99)]
        [InlineData(3, 8, 75.0)]
        public void AddSeats_ShouldCreateSeats(int rows, int seatsPerRow, decimal price)
        {
            //Arrange
            var @event = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));

            //Act
            @event.AddSeats(rows, seatsPerRow, price);

            //Assert
            Assert.Equal(rows * seatsPerRow, @event.Seats.Count);
        }

        [Theory]
        [InlineData(0, 10, 19.99)]
        [InlineData(5, 0, 19.99)]
        [InlineData(5, 10, -5.0)]
        public void AddSeats_ShouldThrowDomainException_ForInvalidInput(int rows, int seatsPerRow, decimal price)
        {
            //Arrange
            var @event = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            //Act & Assert
            var exception = Assert.Throws<DomainException>(() => @event.AddSeats(rows, seatsPerRow, price));
            Assert.Equal("InvalidSeatInput", exception.Code);
        }

        [Theory]
        [InlineData(5, 10, 19.99)]
        public void AddSeats_CanOnlyBeCalledOnce(int rows, int seatsPerRow, decimal price)
        {
            //Arrange
            var @event = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            @event.AddSeats(rows, seatsPerRow, price);
            //Act & Assert
            var exception = Assert.Throws<DomainException>(() => @event.AddSeats(rows, seatsPerRow, price));
            Assert.Equal("SeatsAlreadyAdded", exception.Code);
        }

        [Fact]
        public void Release_ShouldThrowDomainException_WhenSeatIsNotReserved()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);

            //Act
            var seat = newEvent.Seats.Single();

            //Assert
            var exception = Assert.Throws<DomainException>(seat.Release);
            Assert.Equal("SeatNotReserved", exception.Code);
        }

        [Fact]
        public void Release_ShouldChangeStatusToAvailable_WhenSeatIsReserved()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);
            //Act
            var seat = newEvent.Seats.Single();
            seat.Reserve();
            seat.Release();
            //Assert
            Assert.Equal(SeatStatus.Available, seat.Status);
        }

        [Fact]
        public void Reserve_ShouldThrowDomainException_WhenSeatIsNotAvailable()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);

            //Act
            var seat = newEvent.Seats.Single();
            seat.Reserve();

            // Assert
            var exception = Assert.Throws<DomainException>(seat.Reserve);
            Assert.Equal("SeatNotAvailable", exception.Code);
        }

        [Fact]
        public void Reserve_ShouldChangeStatusToReserved_WhenSeatIsAvailable()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);
            //Act
            var seat = newEvent.Seats.Single();
            seat.Reserve();
            //Assert
            Assert.Equal(SeatStatus.Reserved, seat.Status);
        }

        [Fact]
        public void Sell_ShouldThrowDomainException_WhenSeatIsNotReserved()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);

            //Act
            var seat = newEvent.Seats.Single();

            //Assert
            var exception = Assert.Throws<DomainException>(seat.Sell);
            Assert.Equal("SeatNotReserved", exception.Code);
        }

        [Fact]
        public void Sell_ShouldChangeStatusToSold_WhenSeatIsReserved()
        {
            //Arrange
            var newEvent = new Event("Concert", "Stadium", DateTime.UtcNow.AddDays(10));
            newEvent.AddSeats(1, 1, 50.0m);
            //Act
            var seat = newEvent.Seats.Single();
            seat.Reserve();
            seat.Sell();
            //Assert
            Assert.Equal(SeatStatus.Sold, seat.Status);
        }
    }
}
