using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Bus;
using EventHorizon.Game.Server.Core.Bus.Event;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Player.ZoneChanged.Handler
{
    /// <summary>
    /// Notifies the player that they have changed their Zone.
    /// </summary>
    /// <typeparam name="PlayerZoneChangedEvent"></typeparam>
    public class PlayerZoneChangedHandler : INotificationHandler<PlayerZoneChangedEvent>
    {
        readonly IMediator _mediator;
        readonly IHubContext<CoreBus, ITypedCoreHub> _coreBusContext;
        public PlayerZoneChangedHandler(IMediator mediator, IHubContext<CoreBus, ITypedCoreHub> coreBusContext)
        {
            _mediator = mediator;
            _coreBusContext = coreBusContext;
        }
        public async Task Handle(PlayerZoneChangedEvent notification, CancellationToken cancellationToken)
        {
            await _coreBusContext.Clients.User(notification.Player.Id).PlayerZoneChanged(notification.Zone);
        }
    }
}