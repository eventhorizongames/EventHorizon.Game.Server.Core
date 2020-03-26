using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Connection;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.UpdatePlayer.Handler
{
    public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IPlayerConnectionFactory _connectionFactory;

        public UpdatePlayerHandler(
            IPlayerConnectionFactory connectionFactory
        )
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Unit> Handle(
            UpdatePlayerCommand request,
            CancellationToken cancellationToken
        )
        {
            await (await _connectionFactory.GetConnection())
                .SendAction("UpdatePlayer", request.Player);
            return Unit.Value;
        }
    }
}