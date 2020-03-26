using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register
{
    public struct RegisterZoneEvent : IRequest<ZoneDetails>
    {
        public ZoneDetails Zone { get; set; }
    }
}