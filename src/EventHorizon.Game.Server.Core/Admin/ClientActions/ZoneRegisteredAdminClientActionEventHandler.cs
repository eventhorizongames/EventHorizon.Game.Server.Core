namespace EventHorizon.Game.Server.Core.Admin.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Admin.Bus;
using EventHorizon.Game.Server.Core.Zone.Register;

using MediatR;

using Microsoft.AspNetCore.SignalR;

public class ZoneRegisteredAdminClientActionEventHandler
    : INotificationHandler<ZoneRegisteredEvent>
{
    private readonly IHubContext<AdminBus> _hubContext;

    public ZoneRegisteredAdminClientActionEventHandler(
        IHubContext<AdminBus> hubContext
    )
    {
        _hubContext = hubContext;
    }

    public async Task Handle(
        ZoneRegisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _hubContext.Clients.All.SendAsync(
            "ZoneRegistered",
            notification.Zone,
            cancellationToken: cancellationToken
        );
    }
}
