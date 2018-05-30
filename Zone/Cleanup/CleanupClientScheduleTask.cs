using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Schedule;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EventHorizon.Game.Server.Core.Zone.Cleanup
{
    public class CleanupClientScheduleTask : IScheduledTask
    {
        public string Schedule => "*/15 * * * * *";
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public CleanupClientScheduleTask(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using (var serviceScope = _serviceScopeFactory.CreateScope())
            {
                await serviceScope.ServiceProvider.GetService<IMediator>().Publish(new CleanupAllClientsEvent());
            }
        }
    }
}