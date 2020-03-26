namespace EventHorizon.Game.Server.Core.Zone.Cleanup
{
    using MediatR;
    using EventHorizon.TimerService;
    using EventHorizon.Game.Server.Core.Started;

    public class CleanupOldZonesTimerTask : ITimerTask
    {
        public int Period { get; } = 60 * 1000; // 60 seconds
        public string Tag { get; } = "CleanupOldZonesTimerTask";
        public IRequest<bool> OnValidationEvent { get; } = new IsServerStarted();
        public INotification OnRunEvent { get; } = new CleanupOldZonesEvent();
    }
}