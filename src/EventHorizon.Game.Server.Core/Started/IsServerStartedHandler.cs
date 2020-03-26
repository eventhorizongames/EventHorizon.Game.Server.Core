namespace EventHorizon.Game.Server.Core.Started
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.State;
    using MediatR;

    public class IsServerStartedHandler : IRequestHandler<IsServerStarted, bool>
    {
        private readonly ServerState _serverState;

        public IsServerStartedHandler(
            ServerState serverState
        )
        {
            _serverState = serverState;
        }

        public Task<bool> Handle(
            IsServerStarted request,
            CancellationToken cancellationToken
        )
        {
            return Task.FromResult(
                _serverState.IsServerStarted
            );
        }
    }
}