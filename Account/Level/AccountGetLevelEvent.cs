using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Zone
{
    public struct AccountGetZoneEvent : IRequest<ZoneDetails>
    {
        public string AccountId { get; set; }
    }
}