namespace EventHorizon.Game.Server.Core.Admin.MovePlayer.Event.Handler
{
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Player.Events.Details;
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
    using EventHorizon.Game.Server.Core.Player.ZoneChanged;
    using EventHorizon.Game.Server.Core.Zone.Details;
    using MediatR;

    public class AdminMovePlayerByZoneIdHandler : INotificationHandler<AdminMovePlayerByZoneIdEvent>
    {
        private readonly IMediator _mediator;

        public AdminMovePlayerByZoneIdHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task Handle(
            AdminMovePlayerByZoneIdEvent notification, 
            CancellationToken cancellationToken
        )
        {
            // Get Player
            var player = await _mediator.Send(
                new QueryPlayerDetailsById(
                    notification.PlayerId
                )
            );
            // Get Zone
            var zone = await _mediator.Send(
                new FindZoneDetailsBasedOnLocation(
                    new LocationState
                    {
                        CurrentZone = notification.ZoneId,
                    }
                )
            );

            // Move player to Zone
            if (player.IsFound() && zone.IsFound())
            {
                // Update player location
                var location = player.Location;
                location.CurrentZone = zone.Id;
                player.Location = location;
                
                // Update player transform
                var transform = player.Transform;
                transform.Position = Vector3.Zero;
                player.Transform = transform;

                await _mediator.Send(
                    new UpdatePlayerCommand(
                        player
                    )
                );
                await _mediator.Publish(
                    new PlayerZoneChangedEvent
                    {
                        Player = player,
                        Zone = zone,
                    }
                );
            }
        }
    }
}