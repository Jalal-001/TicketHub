using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TicketHub.Infrastructure
{
    public static class Infrastructure
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            var ticketHubConnectionString = configuration.GetConnectionString("TicketHub") ?? throw new InvalidOperationException("TicketHub connection string is not configured.");
            var redisConnectionString = configuration.GetConnectionString("Redis") ?? throw new InvalidOperationException("Redis connection string is not configured.");

            services.AddHealthChecks()
                .AddSqlServer(ticketHubConnectionString)
                .AddRedis(redisConnectionString);

            services.AddDbContext<Databases.MSSQL.DbContexts.TicketHubDbContext>(options =>
            {
                options.UseSqlServer(ticketHubConnectionString);

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
            return services;
        }
    }
}
