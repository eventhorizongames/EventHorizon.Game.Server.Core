using System.Collections.Generic;
using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Details
{
    public struct AllZoneDetailsEvent : IRequest<IEnumerable<ZoneDetails>>
    {
        
    }
}