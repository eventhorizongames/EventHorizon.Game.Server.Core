using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Client.UpdatePlayerProfile
{
    public struct UpdatePlayerProfileFromClientEvent : INotification
    {
        public string PlayerId { get; set; }
        public string ConnectionId { get; set; }
        public PlayerProfile Player { get; set; }
    }
}