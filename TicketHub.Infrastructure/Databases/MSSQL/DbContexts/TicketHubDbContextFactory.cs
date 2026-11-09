using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TicketHub.Infrastructure.Databases.MSSQL.DbContexts
{
    public class TicketHubDbContextFactory : IDesignTimeDbContextFactory<TicketHubDbContext>
    {
        public TicketHubDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                    .AddUserSecrets<TicketHubDbContextFactory>()
                                    .AddEnvironmentVariables()
                                    .Build();

            var connectionString = configuration.GetConnectionString("TicketHub")
                ?? throw new InvalidOperationException("TicketHub connection string is not configured.");

            var options = new DbContextOptionsBuilder<TicketHubDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new TicketHubDbContext(options);
        }
    }
}
