namespace EventHorizon.Game.Server.Core.Zone.Register.Handler;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;

using MediatR;

public class RegisterZoneHandler
    : IRequestHandler<RegisterZoneEvent, ZoneDetails>
{
    private readonly IPublisher _publisher;
    private readonly IZoneRepository _zoneRepository;

    public RegisterZoneHandler(
        IPublisher publisher,
        IZoneRepository zoneRepository
    )
    {
        _publisher = publisher;
        _zoneRepository = zoneRepository;
    }

    public async Task<ZoneDetails> Handle(
        RegisterZoneEvent notification,
        CancellationToken cancellationToken
    )
    {
        var zoneDetails = MapToDetails(
            await _zoneRepository.Add(MapToEntity(notification.Zone))
        );

        await _publisher.Publish(
            new ZoneRegisteredEvent(zoneDetails),
            cancellationToken
        );

        return zoneDetails;
    }

    private static ZoneEntity MapToEntity(ZoneDetails details)
    {
        return new ZoneEntity
        {
            Id = details.Id,
            ConnectionId = details.ConnectionId,
            ServerAddress = details.ServerAddress,
            Tag = details.Tag,
            LastPing = DateTime.Now,
            ServiceDetails = details.Details,
        };
    }

    private static ZoneDetails MapToDetails(ZoneEntity entity)
    {
        return new ZoneDetails
        {
            Id = entity.Id,
            ConnectionId = entity.ConnectionId,
            ServerAddress = entity.ServerAddress,
            Tag = entity.Tag,
            Details = entity.ServiceDetails,
        };
    }
}
