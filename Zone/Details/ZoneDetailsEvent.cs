using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Details
{
    public struct ZoneDetailsEvent : IRequest<ZoneDetails>
    {
         public string Id { get; set; }
    }
}