using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TicketHub.Application.Events;

namespace TicketHub.Application
{
    public static class Application
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services
            services.AddScoped<IEventService, EventService>();

            services.AddValidatorsFromAssembly(typeof(Application).Assembly);


            return services;
        }
    }
}
