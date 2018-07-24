using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register
{
    public class UnregisterZoneByConnectionIdEvent : INotification
    {
        public string ConnectionId { get; set; }
    }
}