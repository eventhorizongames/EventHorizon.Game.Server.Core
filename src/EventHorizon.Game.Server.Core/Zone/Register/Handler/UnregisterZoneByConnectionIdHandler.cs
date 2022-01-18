namespace EventHorizon.Game.Server.Core.Zone.Register.Handler;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Repo;

using MediatR;

public class UnregisterZoneByConnectionIdHandler
    : INotificationHandler<UnregisterZoneByConnectionIdEvent>
{
    private readonly IPublisher _publisher;
    private readonly IZoneRepository _zoneRepository;

    public UnregisterZoneByConnectionIdHandler(
        IPublisher publisher,
        IZoneRepository zoneRepository
    )
    {
        _publisher = publisher;
        _zoneRepository = zoneRepository;
    }

    public async Task Handle(
        UnregisterZoneByConnectionIdEvent notification,
        CancellationToken cancellationToken
    )
    {
        var zone = await _zoneRepository.Find(
            zone => zone.ConnectionId == notification.ConnectionId
        );

        if (!zone.IsFound())
        {
            return;
        }

        await _zoneRepository.Remove(zone);

        await _publisher.Publish(
            new ZoneUnregisteredEvent(zone.Id),
            cancellationToken
        );
    }
}
