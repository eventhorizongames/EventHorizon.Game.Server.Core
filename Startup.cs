using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Account.Repo.Impl;
using EventHorizon.Game.Server.Core.ExceptionFilter;
using EventHorizon.Schedule;
using EventHorizon.Game.Server.Core.Zone.Cleanup;
using EventHorizon.Game.Server.Core.Zone.Repo;
using EventHorizon.Game.Server.Core.Zone.Repo.Impl;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventHorizon.Game.Server.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

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
                });
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(JsonExceptionFilter));
            });

            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountZoneRepository, AccountZoneRepository>();

            services.AddSingleton<IScheduledTask, CleanupClientScheduleTask>();

            services.AddScheduler((sender, args) =>
            {
                Console.WriteLine(args.Exception.Message);
                args.SetObserved();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}