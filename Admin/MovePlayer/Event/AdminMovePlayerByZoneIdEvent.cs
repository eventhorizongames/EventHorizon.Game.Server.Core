using MediatR;

namespace EventHorizon.Game.Server.Core.Admin.MovePlayer.Event
{
    public class AdminMovePlayerByZoneIdEvent : INotification
    {
        public string PlayerId { get; set; }
        public string ZoneId { get; set; }
    }
}