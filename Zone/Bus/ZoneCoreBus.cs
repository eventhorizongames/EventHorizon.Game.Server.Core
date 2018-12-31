using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Bus.Event;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Ping;
using EventHorizon.Game.Server.Core.Zone.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Zone.Bus
{
    [Authorize]
    public class ZoneCoreBus : Hub<ITypedZoneCoreHub>
    {
        readonly IMediator _mediator;
        public ZoneCoreBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.ConnectionId);
            await _mediator.Publish(new UnregisterZoneByConnectionIdEvent
            {
                ConnectionId = Context.ConnectionId
            });
        }
        public async Task<ZoneRegistered> RegisterZone(ZoneRegistrationDetails zoneDetails)
        {
            var Zone = await _mediator.Send(new RegisterZoneEvent
            {
                Zone = new ZoneDetails
                {
                    ConnectionId = Context.ConnectionId,
                    ServerAddress = zoneDetails.ServerAddress,
                    Tags = zoneDetails.Tags,
                }
            });
            return new ZoneRegistered
            {
                Id = Zone.Id,
            };
        }
        public async Task Ping()
        {
            await _mediator.Publish(new PingZoneEvent
            {
                ConnectionId = Context.ConnectionId
            });
        }
    }
    public interface ITypedZoneCoreHub
    {
    }
}