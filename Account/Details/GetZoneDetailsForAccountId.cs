namespace EventHorizon.Game.Server.Core.Account.Zone
{
    using EventHorizon.Game.Server.Core.Zone.Model;
    using MediatR;

    public struct GetZoneDetailsForAccountId : IRequest<ZoneDetails>
    {
        public string AccountId { get; }

        public GetZoneDetailsForAccountId(
            string accountId
        )
        {
            AccountId = accountId;
        }
    }
}