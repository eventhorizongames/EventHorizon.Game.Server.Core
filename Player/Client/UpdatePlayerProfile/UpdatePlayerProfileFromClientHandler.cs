using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Bus;
using EventHorizon.Game.Server.Core.Player.Client.SendPlayerProfile;
using EventHorizon.Game.Server.Core.Player.Events.Details;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Player.Client.UpdatePlayerProfile
{
    public class UpdatePlayerProfileFromClientHandler : INotificationHandler<UpdatePlayerProfileFromClientEvent>
    {
        readonly IMediator _mediator;
        readonly IHubContext<PlayerBus, ITypedClientPlayerBus> _playerBus;
        public UpdatePlayerProfileFromClientHandler(
            IMediator mediator,
            IHubContext<PlayerBus, ITypedClientPlayerBus> bus
        )
        {
            _mediator = mediator;
            _playerBus = bus;
        }
        public async Task Handle(UpdatePlayerProfileFromClientEvent notification, CancellationToken cancellationToken)
        {
            var connectionId = notification.ConnectionId;
            var playerId = notification.PlayerId;
            var playerDetails = await _mediator.Send(
                new PlayerGetDetailsEvent
                {
                    Id = playerId
                }
            );
            if (!playerDetails.IsFound())
            {
                await _playerBus.Clients.Client(connectionId).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = false,
                        MessageCode = "player_not_found",
                    }
                ).ConfigureAwait(false);
                return;
            }

            if (await ValidPlayerProperties(notification.ConnectionId, notification.Player))
            {
                playerDetails.Name = notification.Player.Name;
                playerDetails.Locale = notification.Player.Locale;

                await _mediator.Publish(
                    new UpdatePlayerEvent
                    {
                        Player = playerDetails
                    }
                ).ConfigureAwait(false);

                await _playerBus.Clients.Client(connectionId).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = true,
                        Player = notification.Player
                    }
                ).ConfigureAwait(false);
            }
        }
        private async Task<bool> ValidPlayerProperties(string connectionId, PlayerProfile player)
        {
            // Simple validation added for testing.
            if (player.Name == "@$$")
            {
                await _playerBus.Clients.Client(connectionId).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = false,
                        MessageCode = "player_name_not_valid",
                    }
                ).ConfigureAwait(false);
                return false;
            }
            return true;
        }
    }
}