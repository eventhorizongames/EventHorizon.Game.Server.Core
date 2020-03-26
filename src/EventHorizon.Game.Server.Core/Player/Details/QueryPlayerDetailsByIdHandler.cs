namespace EventHorizon.Game.Server.Core.Player.Events.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Player.Connection;
    using EventHorizon.Game.Server.Core.Player.Model;
    using MediatR;

    public class QueryPlayerDetailsByIdHandler : IRequestHandler<QueryPlayerDetailsById, PlayerDetails>
    {
        private readonly IPlayerConnectionFactory _connectionFactory;

        public QueryPlayerDetailsByIdHandler(
            IPlayerConnectionFactory connectionFactory
        )
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<PlayerDetails> Handle(
            QueryPlayerDetailsById request,
            CancellationToken cancellationToken
        )
        {
            return await (
                await _connectionFactory.GetConnection()
            ).SendAction<PlayerDetails>(
                "GetPlayer",
                request.Id
            );
        }
    }
}