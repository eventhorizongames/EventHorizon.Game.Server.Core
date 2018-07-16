using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Player.Connection;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.UpdatePlayer.Handler
{
    public class UpdatePlayerHandler : INotificationHandler<UpdatePlayerEvent>
    {
        readonly IPlayerConnectionFactory _connectionFactory;
        public UpdatePlayerHandler(IPlayerConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task Handle(UpdatePlayerEvent notification, CancellationToken cancellationToken)
        {
            var connection = await _connectionFactory.GetConnection();
            await connection.SendAction("UpdatePlayer", notification.Player);
        }
    }
}