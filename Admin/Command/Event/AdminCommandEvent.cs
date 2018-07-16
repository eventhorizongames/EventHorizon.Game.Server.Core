using EventHorizon.Game.Server.Core.Admin.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Admin.Command
{
    public class AdminCommandEvent : IRequest<AdminActionResponse>
    {
        public string Command { get; set; }
    }
}