using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register.Handler
{
    public class UnregisterZoneByConnectionIdHandler : INotificationHandler<UnregisterZoneByConnectionIdEvent>
    {
        private readonly IZoneRepository _zoneRepository;

        public UnregisterZoneByConnectionIdHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(UnregisterZoneByConnectionIdEvent notification, CancellationToken cancellationToken)
        {
            await _zoneRepository.Remove(await _zoneRepository.Find(zone => zone.ConnectionId == notification.ConnectionId));
        }
    }
}