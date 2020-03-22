namespace EventHorizon.Game.Server.Core.Player.Client.UpdatePlayerProfile
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Player.Bus;
    using EventHorizon.Game.Server.Core.Player.Events.Details;
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;

    public class UpdatePlayerProfileFromClientHandler : INotificationHandler<UpdatePlayerProfileFromClientEvent>
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<PlayerBus, ITypedClientPlayerBus> _playerBus;

        public UpdatePlayerProfileFromClientHandler(
            IMediator mediator,
            IHubContext<PlayerBus, ITypedClientPlayerBus> bus
        )
        {
            _mediator = mediator;
            _playerBus = bus;
        }

        public async Task Handle(
            UpdatePlayerProfileFromClientEvent notification, 
            CancellationToken cancellationToken
        )
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
                await _playerBus.Clients.Client(
                    connectionId
                ).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = false,
                        MessageCode = "player_not_found",
                    }
                );
                return;
            }

            if (await ValidPlayerProperties(
                notification.ConnectionId, 
                notification.Player
            ))
            {
                playerDetails.Name = notification.Player.Name;
                playerDetails.Locale = notification.Player.Locale;

                await _mediator.Send(
                    new UpdatePlayerCommand(
                        playerDetails
                    )
                );

                await _playerBus.Clients.Client(
                    connectionId
                ).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = true,
                        Player = notification.Player
                    }
                );
            }
        }

        private async Task<bool> ValidPlayerProperties(
            string connectionId, 
            PlayerProfile player
        )
        {
            // Simple validation added for testing.
            if (player.Name == "@$$") // TODO: Flush this out with more Validation
            {
                await _playerBus.Clients.Client(
                    connectionId
                ).PlayerUpdated(
                    new PlayerClientResponse
                    {
                        Success = false,
                        MessageCode = "player_name_not_valid",
                    }
                );
                return false;
            }
            return true;
        }
    }
}