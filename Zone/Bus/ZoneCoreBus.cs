using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Bus.Event;
using EventHorizon.Game.Server.Core.Zone.Exceptions;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Ping;
using EventHorizon.Game.Server.Core.Zone.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EventHorizon.Game.Server.Core.Zone.Bus
{
    [Authorize]
    public class ZoneCoreBus : Hub<ITypedZoneCoreHub>
    {
        readonly ILogger _logger;
        readonly IMediator _mediator;
        public ZoneCoreBus(
            ILogger<ZoneCoreBus> logger,
            IMediator mediator)
        {
            _logger = logger;
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
            try
            {
                var Zone = await _mediator.Send(new RegisterZoneEvent
                {
                    Zone = new ZoneDetails
                    {
                        Id = zoneDetails.Id,
                        ServerAddress = zoneDetails.ServerAddress,
                        Tags = zoneDetails.Tags,

                        ConnectionId = Context.ConnectionId,
                    }
                });
                return new ZoneRegistered(Zone.Id);

            }
            catch (ZoneIdInvalidException ex)
            {
                _logger.LogError("Failed to RegisterZone, Id invalid.", ex);
                return new ZoneRegistered(zoneDetails.Id, ex.Code);
            }
            catch (ZoneExistsException ex)
            {
                _logger.LogError("Failed to RegisterZone, Already Exists.", ex);
                return new ZoneRegistered(zoneDetails.Id, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to RegisterZone, General Exception.", ex);
                return new ZoneRegistered(zoneDetails.Id, "general");
            }
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