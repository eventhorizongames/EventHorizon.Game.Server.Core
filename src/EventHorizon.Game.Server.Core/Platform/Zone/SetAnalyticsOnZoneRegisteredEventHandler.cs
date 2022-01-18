namespace EventHorizon.Game.Server.Core.Platform.Zone;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Register;
using EventHorizon.Game.Server.Core.Zone.Repo;
using EventHorizon.Platform.Api;

using MediatR;

public class SetAnalyticsOnZoneRegisteredEventHandler
    : INotificationHandler<ZoneRegisteredEvent>
{
    private readonly PlatformAnalytics _platformAnalytics;
    private readonly IZoneRepository _repository;

    public SetAnalyticsOnZoneRegisteredEventHandler(
        PlatformAnalytics platformAnalytics,
        IZoneRepository repository
    )
    {
        _platformAnalytics = platformAnalytics;
        _repository = repository;
    }

    public Task Handle(
        ZoneRegisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        _platformAnalytics.Set("ConnectedZones", _repository.Count.ToString());

        return Task.CompletedTask;
    }
}
