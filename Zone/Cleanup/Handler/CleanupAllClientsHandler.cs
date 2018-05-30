using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Cleanup.Handler
{
    public class CleanupAllClientsHandler : INotificationHandler<CleanupAllClientsEvent>
    {
        private readonly IZoneRepository _zoneRepository;
        public CleanupAllClientsHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }
        public async Task Handle(CleanupAllClientsEvent notification, CancellationToken cancellationToken)
        {
            var allZones = await _zoneRepository.GetAll();
        }
    }
}