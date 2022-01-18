namespace EventHorizon.Game.Server.Core.Platform.Zone;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Register;
using EventHorizon.Game.Server.Core.Zone.Repo;
using EventHorizon.Platform.Api;

using MediatR;

public class SetAnalyticsOnZoneUnregisteredEventHandler
    : INotificationHandler<ZoneUnregisteredEvent>
{
    private readonly PlatformAnalytics _platformAnalytics;
    private readonly IZoneRepository _repository;

    public SetAnalyticsOnZoneUnregisteredEventHandler(
        PlatformAnalytics platformAnalytics,
        IZoneRepository repository
    )
    {
        _platformAnalytics = platformAnalytics;
        _repository = repository;
    }

    public Task Handle(
        ZoneUnregisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        _platformAnalytics.Set("ConnectedZones", _repository.Count.ToString());

        return Task.CompletedTask;
    }
}
