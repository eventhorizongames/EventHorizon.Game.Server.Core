using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Details
{
    public struct PlayerGetDetailsEvent : IRequest<PlayerDetails>
    {
        public string Id { get; set; }
    }
}