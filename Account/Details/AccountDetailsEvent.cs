using EventHorizon.Game.Server.Core.Account.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Details
{
    public struct AccountDetailsEvent : IRequest<AccountDetails>
    {
        public string Id { get; set; }
    }
}