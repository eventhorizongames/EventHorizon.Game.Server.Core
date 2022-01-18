namespace EventHorizon.Platform;

using System;

using EventHorizon.Platform.Api;
using EventHorizon.Platform.Model;
using EventHorizon.Platform.State;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

public static class PlatformExtensions
{
    public static IServiceCollection AddPlatformAnalytics(
        this IServiceCollection services
    ) => services.AddSingleton<PlatformAnalytics, InMemoryPlatformAnalytics>();

    public static IEndpointRouteBuilder MapPlatformAnalytics(
        this IEndpointRouteBuilder routes
    )
    {
        routes.MapGet(
            "/platform/analytics",
            async context =>
                await context.Response.WriteAsJsonAsync(
                    context.RequestServices.GetRequiredService<PlatformAnalytics>().Analytics
                )
        );

        return routes;
    }

    public static IEndpointRouteBuilder MapPlatformDetails(
        this IEndpointRouteBuilder routes,
        Action<PlatformDetailsOptions> options
    )
    {
        var option = new PlatformDetailsOptions();

        options(option);

        var platformDetails = new PlatformDetailsModel(option);
        routes.MapGet(
            "/platform/details",
            async context =>
                await context.Response.WriteAsJsonAsync(platformDetails)
        );

        return routes;
    }
}
