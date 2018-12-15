using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Connection;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Zone
{
    public class PlayerFindNewZoneHandler : IRequestHandler<PlayerFindNewZoneEvent, PlayerDetails>
    {
        readonly IMediator _mediator;
        public PlayerFindNewZoneHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<PlayerDetails> Handle(PlayerFindNewZoneEvent request, CancellationToken cancellationToken)
        {
            var player = request.Player;
            player.Position.CurrentZone = await _mediator.Send(new FindFirstZoneIdOfTagEvent
            {
                Tag = player.Position.ZoneTag,
            });
            await _mediator.Publish(new UpdatePlayerEvent
            {
                Player = player,
            });
            return player;
        }
    }
}