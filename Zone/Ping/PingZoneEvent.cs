using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Ping
{
    public class PingZoneEvent : INotification
    {
        public string ConnectionId { get; set; }
    }
}