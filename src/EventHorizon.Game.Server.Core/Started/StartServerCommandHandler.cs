namespace EventHorizon.Game.Server.Core.Started
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.State;
    using MediatR;

    public class StartServerCommandHandler : IRequestHandler<StartServerCommand>
    {
        private readonly ServerState _serverState;

        public StartServerCommandHandler(
            ServerState serverState
        )
        {
            _serverState = serverState;
        }

        public Task<Unit> Handle(
            StartServerCommand request,
            CancellationToken cancellationToken
        )
        {
            _serverState.SetIsServerStarted(
                true
            );
            return Unit.Task;
        }
    }
}