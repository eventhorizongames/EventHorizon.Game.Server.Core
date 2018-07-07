using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Zone
{
    public class PlayerFindNewZoneEvent : IRequest<PlayerDetails>
    {
        public PlayerDetails Player { get; set; }
    }
}