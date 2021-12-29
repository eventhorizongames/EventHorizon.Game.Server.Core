namespace EventHorizon.Game.Server.Core
{
    using EventHorizon.Game.Server.Core.Account.Repo;
    using EventHorizon.Game.Server.Core.Account.Repo.Impl;
    using EventHorizon.Game.Server.Core.Admin.Bus;
    using EventHorizon.Game.Server.Core.Admin.Policies;
    using EventHorizon.Game.Server.Core.Bus;
    using EventHorizon.Game.Server.Core.Logging.ExternalHub;
    using EventHorizon.Game.Server.Core.Player;
    using EventHorizon.Game.Server.Core.Player.Bus;
    using EventHorizon.Game.Server.Core.Player.Connection;
    using EventHorizon.Game.Server.Core.Zone.Bus;
    using EventHorizon.Game.Server.Core.Zone.Cleanup;
    using EventHorizon.Game.Server.Core.Zone.Repo;
    using EventHorizon.Game.Server.Core.Zone.Repo.Impl;
    using EventHorizon.Identity;
    using EventHorizon.Platform;
    using EventHorizon.TimerService;
    using EventHorizon.WebSocket;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Linq;

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
            services.AddMediatR(
                typeof(Startup).Assembly,
                typeof(EventHorizonIdentityExtensions).Assembly
            );

            services.AddHttpClient();
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = HostingEnvironment.IsProduction() || HostingEnvironment.IsStaging();
                    options.Authority = Configuration["Auth:Authority"];
                    options.ApiName = Configuration["Auth:ApiName"];
                    options.TokenRetriever = WebSocketTokenRetriever.FromHeaderAndQueryString;
                });
            services.AddAuthorization(
                options => options.AddUserIdOrClientIdOrAdminPolicy(
                    Configuration["OwnerDetails:UserId"],
                    Configuration["OwnerDetails:PlatformId"]
                )
            );
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

            services.AddSingleton<ITimerTask, CleanupOldZonesTimerTask>();

            services.AddPlayer(Configuration);
            services.AddServerState();

            services.AddTimer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePlayer();
            app.UseServerState();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapPlatformDetails(
                    options => options.SetVersion(
                        Configuration["APPLICATION_VERSION"]
                    )
                );

                routes.MapHub<AdminBus>("/admin");
                routes.MapHub<CoreBus>("/coreBus");
                routes.MapHub<ZoneCoreBus>("/zoneCore");
                routes.MapHub<PlayerBus>("/player");

                routes.MapHub<ClientLoggingHub>("/logging");
            });
        }
    }
}