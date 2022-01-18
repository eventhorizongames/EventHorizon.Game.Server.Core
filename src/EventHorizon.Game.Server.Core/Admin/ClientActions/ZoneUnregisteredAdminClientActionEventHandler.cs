namespace EventHorizon.Game.Server.Core.Admin.ClientActions;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Admin.Bus;
using EventHorizon.Game.Server.Core.Zone.Register;

using MediatR;

using Microsoft.AspNetCore.SignalR;

public class ZoneUnregisteredAdminClientActionEventHandler
    : INotificationHandler<ZoneUnregisteredEvent>
{
    private readonly IHubContext<AdminBus> _hubContext;

    public ZoneUnregisteredAdminClientActionEventHandler(
        IHubContext<AdminBus> hubContext
    )
    {
        _hubContext = hubContext;
    }

    public async Task Handle(
        ZoneUnregisteredEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _hubContext.Clients.All.SendAsync(
            "ZoneUnregistered",
            notification.ZoneId,
            cancellationToken: cancellationToken
        );
    }
}
