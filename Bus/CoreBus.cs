using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Bus.Event;
using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Bus
{
    [Authorize]
    public class CoreBus : Hub<ITypedCoreHub>
    {
        readonly IMediator _mediator;
        public CoreBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
            await _mediator.Publish(new ConnectToCoreEvent
            {
                AccountId = GetPlayerId(),
                ConnectionId = Context.ConnectionId,
            });
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        private string GetPlayerId()
        {
            return Context.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value ?? String.Empty;
        }
    }
    public interface ITypedCoreHub
    {
        Task AccountConnected(AccountDetails accountDetails);
        Task PlayerZoneChanged(ZoneDetails zoneDetails);
    }
}