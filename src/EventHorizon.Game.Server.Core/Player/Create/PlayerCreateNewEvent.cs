namespace EventHorizon.Game.Server.Core.Player.Events.Create
{
    using EventHorizon.Game.Server.Core.Player.Model;
    using MediatR;

    public class PlayerCreateNewEvent : IRequest<PlayerDetails>
    {
        public string Id { get; }

        public PlayerCreateNewEvent(
            string id
        )
        {
            Id = id;
        }
    }
}