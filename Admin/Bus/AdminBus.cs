using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Details;
using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Admin.Bus
{
    public class AdminBus : Hub
    {
        readonly IMediator _mediator;
        public AdminBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnConnectedAsync()
        {
            if (!Context.User.IsInRole("Admin"))
            {
                throw new System.Exception("no_role");
            }
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ZoneDetails>> GetAllZones()
        {
            return await _mediator.Send(new AllZoneDetailsEvent
            {
            });
        }
    }
}