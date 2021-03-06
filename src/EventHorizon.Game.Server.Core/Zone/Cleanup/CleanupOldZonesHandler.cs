namespace EventHorizon.Game.Server.Core.Zone.Cleanup.Handler
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using EventHorizon.Game.Server.Core.Zone.Repo;
    using MediatR;

    public class CleanupOldZonesHandler : INotificationHandler<CleanupOldZonesEvent>
    {
        private static readonly int SECONDS_TO_EXPIRE = 45;

        private readonly IZoneRepository _zoneRepository;
        
        public CleanupOldZonesHandler(
            IZoneRepository zoneRepository
        )
        {
            _zoneRepository = zoneRepository;
        }

        public async Task Handle(
            CleanupOldZonesEvent notification, 
            CancellationToken cancellationToken
        )
        {
            var allZones = await _zoneRepository.GetAll();
            foreach (var zone in allZones)
            {
                if (ShouldZoneBeRemoved(zone))
                {
                    await _zoneRepository.Remove(
                        zone
                    );
                }
            }
        }

        private static bool ShouldZoneBeRemoved(
            ZoneEntity zone
        ) => zone.LastPing.AddSeconds(
            SECONDS_TO_EXPIRE
        ).CompareTo(
            DateTime.Now
        ) < 0;
    }
}