namespace EventHorizon.Game.Server.Core.State
{
    public interface ServerState
    {
        bool IsServerStarted { get; }
        void SetIsServerStarted(bool isServerStarted);
    }
}