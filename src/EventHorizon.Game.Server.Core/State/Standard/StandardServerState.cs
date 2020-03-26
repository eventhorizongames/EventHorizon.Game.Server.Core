namespace EventHorizon.Game.Server.Core.State.Standard
{
    public class StandardServerState : ServerState
    {
        public bool IsServerStarted { get; private set; } = false;

        public void SetIsServerStarted(
            bool isServerStarted
        )
        {
            IsServerStarted = isServerStarted;
        }
    }
}