using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register.Handler
{
    public class UnregisterZoneHandler : INotificationHandler<UnregisterZoneEvent>
    {
        private readonly IZoneRepository _ZoneRepository;

        public UnregisterZoneHandler(IZoneRepository ZoneRepository)
        {
            _ZoneRepository = ZoneRepository;
        }

        public async Task Handle(UnregisterZoneEvent notification, CancellationToken cancellationToken)
        {
            await _ZoneRepository.Remove(await _ZoneRepository.FindById(notification.ZoneId));
        }
    }
}