namespace EventHorizon.Game.Server.Core.Player.Events.Details
{
    using EventHorizon.Game.Server.Core.Player.Model;
    using MediatR;

    public struct QueryPlayerDetailsById : IRequest<PlayerDetails>
    {
        public string Id { get; }

        public QueryPlayerDetailsById(
            string id
        )
        {
            Id = id;
        }
    }
}