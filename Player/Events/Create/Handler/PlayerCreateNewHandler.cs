using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Connection;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Create.Handler
{
    public class PlayerCreateNewHandler : IRequestHandler<PlayerCreateNewEvent, PlayerDetails>
    {
        readonly IMediator _mediator;
        public PlayerCreateNewHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PlayerDetails> Handle(PlayerCreateNewEvent request, CancellationToken cancellationToken)
        {
            var zoneId = await _mediator.Send(new FindFirstZoneIdOfTagEvent
            {
                Tag = "home",
            });
            var newPlayer = new PlayerDetails
            {
                Id = request.Id,
                Position = new PositionState
                {
                    CurrentZone = zoneId,
                    ZoneTag = "home",
                    Position = Vector3.Zero,
                },
                Data = new
                {
                    New = true
                },
            };
            await _mediator.Publish(new UpdatePlayerEvent
            {
                Player = newPlayer,
            });
            return newPlayer;
        }
    }
}