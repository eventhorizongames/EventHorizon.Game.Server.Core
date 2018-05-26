using System;

namespace EventHorizon.Game.Server.Core.Account.Exceptions
{
    public class AccountZoneNotFoundException : Exception
    {
        public string AccountId { get; }

        public AccountZoneNotFoundException(string accountId)
            : base(string.Format("Account Zone Not Found with AccountId of {0}", accountId))
        {
            AccountId = accountId;
        }
    }
}