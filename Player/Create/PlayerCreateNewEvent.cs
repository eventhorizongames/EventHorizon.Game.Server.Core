using EventHorizon.Game.Server.Core.Player.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Events.Create
{
    public class PlayerCreateNewEvent : IRequest<PlayerDetails>
    {
        public string Id { get; set; }
    }
}