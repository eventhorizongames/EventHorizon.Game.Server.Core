using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Events.Details;
using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
using EventHorizon.Game.Server.Core.Player.ZoneChanged;
using EventHorizon.Game.Server.Core.Zone.Details;
using MediatR;

namespace EventHorizon.Game.Server.Core.Admin.MovePlayer.Event.Handler
{
    public class AdminMovePlayerByZoneIdHandler : INotificationHandler<AdminMovePlayerByZoneIdEvent>
    {
        readonly IMediator _mediator;
        public AdminMovePlayerByZoneIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Handle(AdminMovePlayerByZoneIdEvent notification, CancellationToken cancellationToken)
        {
            // Get Player
            var player = await _mediator.Send(new PlayerGetDetailsEvent
            {
                Id = notification.PlayerId,
            });
            // Get Zone
            var zone = await _mediator.Send(new ZoneDetailsEvent
            {
                Id = notification.ZoneId,
            });

            // Move player to Zone
            if (player.IsFound() && zone.IsFound())
            {
                player.Position.CurrentZone = zone.Id;
                player.Position.Position = Vector3.Zero;
                await _mediator.Publish(new UpdatePlayerEvent { Player = player });
                await _mediator.Publish(new PlayerZoneChangedEvent
                {
                    Player = player,
                    Zone = zone,
                });
            }
        }
    }
}