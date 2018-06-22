using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EventHorizon.Game.Server.Core.Player.Connect.Handler
{
    public class ConnectToPlayerServerHandler : INotificationHandler<ConnectToPlayerServerEvent>
    {
        public Task Handle(ConnectToPlayerServerEvent notification, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}