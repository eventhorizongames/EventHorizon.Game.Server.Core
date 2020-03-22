namespace EventHorizon.Game.Server.Core.Player.Events.Zone
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
    using EventHorizon.Game.Server.Core.Zone.Search;
    using MediatR;

    public class PlayerFindNewZoneHandler : IRequestHandler<PlayerFindNewZoneEvent, PlayerDetails>
    {
        readonly IMediator _mediator;

        public PlayerFindNewZoneHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<PlayerDetails> Handle(
            PlayerFindNewZoneEvent request,
            CancellationToken cancellationToken
        )
        {
            var player = request.Player;
            var location = player.Location;

            var newZone = await _mediator.Send(
                new FindFirstZoneIdOfTag(
                    location.ZoneTag
                )
            );
            if (string.IsNullOrEmpty(newZone))
            {
                newZone = await _mediator.Send(
                    new FindFirstZoneIdOfTag(
                        "home"
                    )
                );
            }
            location.CurrentZone = newZone;
            player.Location = location;
            await _mediator.Send(
                new UpdatePlayerCommand(
                    player
                )
            );
            return player;
        }
    }
}