using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Client.SendPlayerProfile
{
    public struct SendPlayerProfileToClientEvent : INotification
    {
        public string PlayerId { get; set; }
        public string ConnectionId { get; set; }
    }
}