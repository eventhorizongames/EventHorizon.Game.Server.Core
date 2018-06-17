using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Bus.Event;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Bus
{
    public class CoreBus : Hub
    {
        readonly IMediator _mediator;
        public CoreBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await _mediator.Publish(new ConnectToCoreEvent
            {
                AccountId = Context.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value,
                ConnectionId = Context.ConnectionId,
            });
        }
    }
}