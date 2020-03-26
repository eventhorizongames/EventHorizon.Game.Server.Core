namespace EventHorizon.Game.Server.Core.Player
{
    using EventHorizon.Game.Server.Core.Started;
    using EventHorizon.Game.Server.Core.State;
    using EventHorizon.Game.Server.Core.State.Standard;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServerStateExtensions
    {
        public static IServiceCollection AddServerState(
            this IServiceCollection services
        )
        {
            return services
                .AddSingleton<ServerState, StandardServerState>()
            ;
        }

        public static IApplicationBuilder UseServerState(
            this IApplicationBuilder app
        )
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var mediator = serviceScope.ServiceProvider.GetService<IMediator>();
                mediator.Send(
                    new StartServerCommand()
                ).GetAwaiter().GetResult();
                return app;
            }
        }
    }
}