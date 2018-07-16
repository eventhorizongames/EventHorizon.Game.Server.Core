using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.UpdatePlayer
{
    public class UpdatePlayerEvent : INotification
    {
        public PlayerDetails Player { get; set; }
    }
}