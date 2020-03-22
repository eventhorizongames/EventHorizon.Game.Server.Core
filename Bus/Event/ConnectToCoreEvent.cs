namespace EventHorizon.Game.Server.Core.Bus.Event
{
    using MediatR;

    public struct ConnectToCoreEvent : INotification
    {
        public string AccountId { get; }
        public string ConnectionId { get; }

        public ConnectToCoreEvent(
            string accountId,
            string connectionId
        )
        {
            AccountId = accountId;
            ConnectionId = connectionId;
        }
    }
}