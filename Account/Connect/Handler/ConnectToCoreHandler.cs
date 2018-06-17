using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Details;
using EventHorizon.Game.Server.Core.Bus;
using EventHorizon.Game.Server.Core.Bus.Event;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Account.Connect.Handler
{
    public class ConnectToCoreHandler : INotificationHandler<ConnectToCoreEvent>
    {
        readonly IMediator _mediator;
        readonly IHubContext<CoreBus> _coreBusContext;
        public ConnectToCoreHandler(IMediator mediator, IHubContext<CoreBus> coreBusContext)
        {
            _mediator = mediator;
            _coreBusContext = coreBusContext;
        }

        public async Task Handle(ConnectToCoreEvent notification, CancellationToken cancellationToken)
        {
            var account = await _mediator.Send(new AccountDetailsEvent
            {
                Id = notification.AccountId,
            });
            await _coreBusContext.Clients.Client(notification.ConnectionId).SendAsync("AccountConnected", account);
        }
    }
}