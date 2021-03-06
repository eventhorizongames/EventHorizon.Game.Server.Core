namespace EventHorizon.Game.Server.Core.Account.Connect.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Account.Details;
    using EventHorizon.Game.Server.Core.Bus;
    using EventHorizon.Game.Server.Core.Bus.Event;
    using EventHorizon.Game.Server.Core.Player.Model;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;
    using MediatR;
    using Microsoft.AspNetCore.SignalR;

    public class ConnectToCoreHandler : INotificationHandler<ConnectToCoreEvent>
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<CoreBus, ITypedCoreHub> _coreBusContext;

        public ConnectToCoreHandler(
            IMediator mediator,
            IHubContext<CoreBus, ITypedCoreHub> coreBusContext
        )
        {
            _mediator = mediator;
            _coreBusContext = coreBusContext;
        }

        public async Task Handle(
            ConnectToCoreEvent notification,
            CancellationToken cancellationToken
        )
        {
            var account = await _mediator.Send(
                new GetAccountDetails(
                    notification.AccountId
                )
            );
            await SetCoreConnectionIdOnPlayer(
                account.Player,
                notification.ConnectionId
            );
            await _coreBusContext.Clients.Client(
                notification.ConnectionId
            ).AccountConnected(
                account
            );
        }

        private async Task SetCoreConnectionIdOnPlayer(
            PlayerDetails playerDetails,
            string coreConnectionId
        )
        {
            playerDetails.Data["CoreConnectionId"] = coreConnectionId;
            await _mediator.Send(
                new UpdatePlayerCommand(
                    playerDetails
                )
            );
        }
    }
}