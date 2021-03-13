namespace EventHorizon.Game.Server.Core
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Serilog.Sinks.Elasticsearch;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Build().Run();
        }

        public static IHostBuilder BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseSerilog((ctx, cfg) => cfg
                        .Enrich.WithProperty("EnvironmentName", ctx.HostingEnvironment.EnvironmentName)
                        .Enrich.WithProperty("PlatformId", ctx.Configuration["OwnerDetails:PlatformId"])
                        .Enrich.WithProperty("ServiceName", "Core")
                        .Enrich.WithProperty("ApplicationVersion", ctx.Configuration["APPLICATION_VERSION"])
                        .ReadFrom.Configuration(ctx.Configuration)
                        .ConfigureElasticsearchLogging(ctx)
                    );
                });
    }

    public static class SerilogElasticsearchExtensions
    {
        public static LoggerConfiguration ConfigureElasticsearchLogging(
            this LoggerConfiguration loggerConfig,
            WebHostBuilderContext context
        )
        {
            if (context.Configuration.GetValue<bool>("Serilog:Elasticsearch:Enabled"))
            {
                var sinkOptions = new ElasticsearchSinkOptions(
                    new Uri(
                        context.Configuration["Elasticsearch:Uri"]
                    )
                )
                {
                    ModifyConnectionSettings = conn =>
                    {
                        conn.BasicAuthentication(
                            context.Configuration["Elasticsearch:Username"],
                            context.Configuration["Elasticsearch:Password"]
                        );
                        return conn;
                    }

                };
                context.Configuration.GetSection(
                    "Serilog:Elasticsearch"
                ).Bind(
                    sinkOptions
                );
                return loggerConfig.WriteTo.Elasticsearch(
                    sinkOptions
                );
            }

            return loggerConfig;
        }
    }
}
