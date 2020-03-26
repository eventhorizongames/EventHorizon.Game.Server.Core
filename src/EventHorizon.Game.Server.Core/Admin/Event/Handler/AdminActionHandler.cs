using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Admin.Command;
using EventHorizon.Game.Server.Core.Admin.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Admin.Event.Handler
{
    public class AdminActionHandler : IRequestHandler<AdminActionEvent, AdminActionResponse>
    {
        readonly IMediator _mediator;
        public AdminActionHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<AdminActionResponse> Handle(AdminActionEvent request, CancellationToken cancellationToken)
        {
            switch (request.Action)
            {
                case "AdminCommand":
                    return await _mediator.Send(new AdminCommandEvent { Command = (string)request.Data });
                default:
                    return new AdminActionResponse { Success = false, Reason = "action_not_found" };
            }
        }
    }
}