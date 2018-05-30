using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Ping.Handler
{
    public class PingZoneHandler : INotificationHandler<PingZoneEvent>
    {
        private readonly IZoneRepository _zoneRepository;
        public PingZoneHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }
        public async Task Handle(PingZoneEvent notification, CancellationToken cancellationToken)
        {
            var zone = await _zoneRepository.FindById(notification.ZoneId);
            zone.LastPing = DateTime.Now;
            await _zoneRepository.Update(zone);
        }
    }
}