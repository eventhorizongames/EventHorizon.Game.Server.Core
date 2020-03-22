namespace EventHorizon.Game.Server.Core.Player.Events.Create
{
    using System.Collections.Generic;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
    using EventHorizon.Game.Server.Core.Zone.Search;
    using MediatR;

    public class PlayerCreateNewHandler : IRequestHandler<PlayerCreateNewEvent, PlayerDetails>
    {
        private readonly IMediator _mediator;

        public PlayerCreateNewHandler(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public async Task<PlayerDetails> Handle(
            PlayerCreateNewEvent request,
            CancellationToken cancellationToken
        )
        {
            var zoneTag = "home"; // TODO: This should come from Settings
            var zoneId = await _mediator.Send(
                new FindFirstZoneIdOfTag(
                    zoneTag
                )
            );
            var newPlayer = new PlayerDetails
            {
                Id = request.Id,
                Location = new LocationState
                {
                    CurrentZone = zoneId,
                    ZoneTag = zoneTag,
                },
                Transform = new TransformState
                {
                    Position = Vector3.Zero,
                },
                Data = new Dictionary<string, object>
                {
                    { "new",  true } // TODO: Change this to Created or a DateTime
                },
            };
            await _mediator.Send(
                new UpdatePlayerCommand(
                    newPlayer
                )
            );
            return newPlayer;
        }
    }
}