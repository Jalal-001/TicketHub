namespace TicketHub.Api.Endpoints
{
    public static class MapEndpointExtension
    {
        public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            IEndpointRouteBuilder endpointRouteBuilder = routeGroupBuilder is not null ? routeGroupBuilder : app;

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(endpointRouteBuilder);
            }
            return app;
        }
    }
}
