namespace EventHorizon.Game.Server.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using EventHorizon.Game.Server.Core.Account.Repo;
    using EventHorizon.Game.Server.Core.Account.Repo.Impl;
    using EventHorizon.Schedule;
    using EventHorizon.Game.Server.Core.Zone.Cleanup;
    using EventHorizon.Game.Server.Core.Zone.Repo;
    using EventHorizon.Game.Server.Core.Zone.Repo.Impl;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using EventHorizon.WebSocket;
    using EventHorizon.Game.Server.Core.Bus;
    using EventHorizon.Game.Server.Core.Player;
    using EventHorizon.Game.Server.Core.Admin.Bus;
    using Microsoft.AspNetCore.SignalR;
    using EventHorizon.Game.Server.Core.Player.Connection;
    using EventHorizon.Game.Server.Core.Zone.Bus;
    using EventHorizon.Game.Server.Core.Player.Bus;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = HostingEnvironment.IsProduction() || HostingEnvironment.IsStaging();
                    options.Authority = Configuration["Auth:Authority"];
                    options.ApiName = Configuration["Auth:ApiName"];
                    options.TokenRetriever = WebSocketTokenRetriever.FromHeaderAndQueryString;
                });
            services.AddRazorPages();
            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();
            services.AddSingleton<IUserIdProvider, SubUserIdProvider>();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                        .WithOrigins(
                            Configuration
                                .GetSection("Cors:Hosts")
                                .GetChildren()
                                .AsEnumerable()
                                .Select(a => a.Value)
                                .ToArray()
                        )
                        .AllowCredentials();
                }));

            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountZoneRepository, AccountZoneRepository>();

            services.AddSingleton<IScheduledTask, CleanupOldZonesScheduledTask>();

            services.AddScheduler((sender, args) =>
            {
                Console.WriteLine(args.Exception.Message);
                args.SetObserved();
            });

            services.AddPlayer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UsePlayer();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapHub<AdminBus>("/admin");
                routes.MapHub<CoreBus>("/coreBus");
                routes.MapHub<ZoneCoreBus>("/zoneCore");
                routes.MapHub<PlayerBus>("/player");
            });
        }
    }
}