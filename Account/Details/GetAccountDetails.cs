namespace EventHorizon.Game.Server.Core.Account.Details
{
    using EventHorizon.Game.Server.Core.Account.Model;
    using MediatR;
    
    public struct GetAccountDetails : IRequest<AccountDetails>
    {
        public string Id { get; }

        public GetAccountDetails(
            string id
        )
        {
            this.Id = id;
        }
    }
}