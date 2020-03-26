using EventHorizon.Game.Server.Core.Admin.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Admin.Event
{
    public class AdminActionEvent : IRequest<AdminActionResponse>
    {
        public string Action { get; set; }
        public object Data { get; set; }
    }
}