namespace EventHorizon.Game.Server.Core.Admin.Bus
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Admin.Event;
    using EventHorizon.Game.Server.Core.Admin.Model;
    using EventHorizon.Game.Server.Core.Admin.Policies;
    using EventHorizon.Game.Server.Core.Zone.Details;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    [Authorize(UserIdOrClientIdOrAdminPolicy.PolicyName)]
    public class AdminBus
        : Hub
    {
        readonly IMediator _mediator;
        public AdminBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<ZoneDetails>> GetAllZones()
        {
            return await _mediator.Send(new AllZoneDetailsEvent
            {
            });
        }

        public async Task<AdminActionResponse> AdminAction(string action, object data)
        {
            return await _mediator.Send(new AdminActionEvent
            {
                Action = action,
                Data = data,
            });
        }
    }
}