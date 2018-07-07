using EventHorizon.Game.Server.Core.Player.Connection;
using EventHorizon.Game.Server.Core.Player.Connection.Impl;
using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventHorizon.Game.Server.Core.Player
{
    public static class PlayerExtensions
    {
        public static IServiceCollection AddPlayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services.Configure<PlayerSettings>(configuration.GetSection("Player"))
                .AddSingleton<IConnectionCache, ConnectionCache>()
                .AddTransient<IPlayerConnectionFactory, PlayerConnectionFactory>();
        }

        public static IApplicationBuilder UsePlayer(this IApplicationBuilder app)
        {
            return app;
        }
    }
}