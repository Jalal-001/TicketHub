using Microsoft.EntityFrameworkCore;

namespace TicketHub.Infrastructure.Databases.MSSQL.DbContexts
{
    public class TicketHubDbContext : DbContext
    {
        public TicketHubDbContext(DbContextOptions<TicketHubDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TicketHubDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
