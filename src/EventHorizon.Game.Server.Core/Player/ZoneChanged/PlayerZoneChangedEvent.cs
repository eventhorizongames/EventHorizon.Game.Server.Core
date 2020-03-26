using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.ZoneChanged
{
    public class PlayerZoneChangedEvent : INotification
    {
        public PlayerDetails Player { get; set; }
        public ZoneDetails Zone { get; set; }
    }
}