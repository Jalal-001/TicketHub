using Microsoft.EntityFrameworkCore;
using TicketHub.Application.Events;
using TicketHub.Application.Events.GetEvents;
using TicketHub.Application.Events.GetEventSeats;
using TicketHub.Domain.Entities;
using TicketHub.Infrastructure.Databases.MSSQL.DbContexts;
using TicketHub.Infrastructure.Implementations;

namespace TicketHub.Infrastructure.Events
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(TicketHubDbContext ticketHubDbContext) : base(ticketHubDbContext)
        {
        }

        public async Task<IReadOnlyList<EventSummaryDto>> GetEvents(CancellationToken cancellationToken)
        {
            var events = await _ticketHubDbContext
                                    .Events
                                    .AsNoTracking()
                                    .Select(e => new EventSummaryDto
                                    {
                                        Id = e.Id,
                                        Name = e.Name,
                                        StartsAt = e.StartsAt,
                                        Venue = e.Venue,
                                        TotalSeats = e.Seats.Count,
                                        AvailableSeats = e.Seats.Count(s => s.Status == SeatStatus.Available)
                                    }).ToListAsync(cancellationToken);
            return events;
        }

        public async Task<EventSeatsDto?> GetEventSeatsAsync(int id, SeatStatus? status, CancellationToken cancellationToken)
        {
            var eventSeats = await _ticketHubDbContext
                                    .Events
                                    .Where(e => e.Id == id)
                                    .AsNoTracking()
                                    .Select(e => new EventSeatsDto
                                    {
                                        Id = e.Id,
                                        Name = e.Name,
                                        Venue = e.Venue,
                                        StartsAt = e.StartsAt,
                                        Seats = e.Seats
                                                .Where(s => status == null || s.Status == status)
                                                .Select(s => new SeatDto
                                                {
                                                    Row = s.Row,
                                                    Number = s.Number,
                                                    Price = s.Price,
                                                    Status = s.Status
                                                })
                                                .OrderBy(s => s.Row)
                                                .ThenBy(s => s.Number)
                                                .ToList()

                                    }).FirstOrDefaultAsync(cancellationToken);


            return eventSeats;
        }
    }
}
