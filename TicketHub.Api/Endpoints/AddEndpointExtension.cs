using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace TicketHub.Api.Endpoints
{
    public static class AddEndpointExtension
    {
        public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
        {
            ServiceDescriptor[] endpointDescriptors = assembly.GetTypes()
                .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .Select(t => new ServiceDescriptor(typeof(IEndpoint), t, ServiceLifetime.Transient))
                .ToArray();

            services.TryAddEnumerable(endpointDescriptors);
            return services;
        }
    }
}
