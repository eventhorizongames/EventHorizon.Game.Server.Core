using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Connection;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Zone.Handler
{
    public class PlayerFindNewZoneHandler : IRequestHandler<PlayerFindNewZoneEvent, PlayerDetails>
    {
        readonly IMediator _mediator;
        readonly IPlayerConnectionFactory _connectionFactory;
        public PlayerFindNewZoneHandler(IMediator mediator, IPlayerConnectionFactory connectionFactory)
        {
            _mediator = mediator;
            _connectionFactory = connectionFactory;
        }
        public async Task<PlayerDetails> Handle(PlayerFindNewZoneEvent request, CancellationToken cancellationToken)
        {
            var player = request.Player;
            var connection = await _connectionFactory.GetConnection();
            player.Position.CurrentZone = await _mediator.Send(new FindFirstZoneIdOfTagEvent
            {
                Tag = player.Position.ZoneTag,
            });
            await connection.SendAction("UpdatePlayer", player);
            return player;
        }
    }
}