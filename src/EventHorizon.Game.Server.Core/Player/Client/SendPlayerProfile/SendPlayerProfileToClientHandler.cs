using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Bus;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Player.Events.Details;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Player.Client.SendPlayerProfile
{
    public class SendPlayerProfileToClientHandler : INotificationHandler<SendPlayerProfileToClientEvent>
    {
        readonly IMediator _mediator;
        readonly IHubContext<PlayerBus, ITypedClientPlayerBus> _playerBus;
        public SendPlayerProfileToClientHandler(
            IMediator mediator,
            IHubContext<PlayerBus, ITypedClientPlayerBus> bus
        )
        {
            _mediator = mediator;
            _playerBus = bus;
        }
        public async Task Handle(SendPlayerProfileToClientEvent notification, CancellationToken cancellationToken)
        {
            var connectionId = notification.ConnectionId;
            var playerId = notification.PlayerId;
            var playerDetails = await _mediator.Send(
                new QueryPlayerDetailsById(
                    playerId
                )
            );
            if (!playerDetails.IsFound())
            {
                await _playerBus.Clients.Client(connectionId).PlayerProfile(
                    new PlayerClientResponse
                    {
                        Success = false,
                        MessageCode = "player_not_found",
                    }
                ).ConfigureAwait(false);
                return;
            }

            await _playerBus.Clients.Client(connectionId).PlayerProfile(
                new Model.PlayerClientResponse
                {
                    Success = true,
                    Player = new PlayerProfile
                    {
                        Name = playerDetails.Name,
                        Locale = playerDetails.Locale,
                    }
                }
            ).ConfigureAwait(false);
        }
    }
}