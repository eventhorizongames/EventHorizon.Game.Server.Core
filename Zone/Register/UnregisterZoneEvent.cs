using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register
{
    public class UnregisterZoneEvent : INotification
    {
        public string ZoneId { get; set; }
    }
}