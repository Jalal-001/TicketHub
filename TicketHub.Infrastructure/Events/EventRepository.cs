using TicketHub.Application.Events;
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
    }
}
