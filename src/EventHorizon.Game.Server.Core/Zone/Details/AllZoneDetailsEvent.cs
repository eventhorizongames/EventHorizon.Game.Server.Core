namespace EventHorizon.Game.Server.Core.Zone.Details
{
    using System.Collections.Generic;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using MediatR;

    public struct AllZoneDetailsEvent : IRequest<IEnumerable<ZoneDetails>>
    {

    }
}