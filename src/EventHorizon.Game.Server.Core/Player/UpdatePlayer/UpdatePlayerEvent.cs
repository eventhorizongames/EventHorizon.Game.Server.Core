namespace EventHorizon.Game.Server.Core.Player.UpdatePlayer
{
    using EventHorizon.Game.Server.Core.Player.Model;
    using MediatR;

    public class UpdatePlayerCommand : IRequest
    {
        public PlayerDetails Player { get; }

        public UpdatePlayerCommand(
            PlayerDetails player
        )
        {
            Player = player;
        }
    }
}