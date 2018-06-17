using MediatR;

namespace EventHorizon.Game.Server.Core.Bus.Event
{
    public struct ConnectToCoreEvent : INotification
    {
        public string AccountId { get; internal set; }
        public string ConnectionId { get; internal set; }
    }
}