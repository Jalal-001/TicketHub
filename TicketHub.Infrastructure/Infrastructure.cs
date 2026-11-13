using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketHub.Application.Abstractions;
using TicketHub.Application.Events;
using TicketHub.Domain.Entities;
using TicketHub.Infrastructure.Databases.MSSQL.DbContexts;
using TicketHub.Infrastructure.Events;

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

            services.AddDbContext<TicketHubDbContext>(options =>
            {
                options.UseSqlServer(ticketHubConnectionString);

                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
                options.AddInterceptors(new Interceptors.AuditableEntityInterceptor());

                options.UseAsyncSeeding(async (context, _, cancellationToken) =>
                {
                    if (!await context.Set<Seat>().AnyAsync(cancellationToken))
                    {
                        var newEvent = SeedData();
                        await context.Set<Event>().AddAsync(newEvent, cancellationToken);
                        await context.SaveChangesAsync(cancellationToken);
                    }
                }).UseSeeding((context, _) =>
                {
                    if (!context.Set<Seat>().Any())
                    {
                        var newEvent = SeedData();
                        context.Set<Event>().Add(newEvent);
                        context.SaveChanges();
                    }
                });

                Event SeedData()
                {
                    var newEvent = new Event
                    {
                        Name = "Sample event",
                        Venue = "Sample palace",
                        StartsAt = DateTime.UtcNow.AddMonths(1)
                    };

                    for (int j = 1; j <= 20; j++)
                    {
                        for (int k = 1; k <= 50; k++)
                        {
                            var seat = new Seat
                            {
                                Row = j,
                                Number = k,
                                Price = 19.99m
                            };
                            newEvent.Seats.Add(seat);
                        }
                    }
                    return newEvent;
                }
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TicketHubDbContext>());

            // Add repositories
            services.AddScoped<IEventRepository, EventRepository>();

            return services;
        }
    }
}
