using System;

namespace EventHorizon.Game.Server.Core.Account.Exceptions
{
    public class AccountLevelNotFoundException : Exception
    {
        public string AccountId { get; }

        public AccountLevelNotFoundException(string accountId)
            : base(string.Format("Account Level Not Found with AccountId of {0}", accountId))
        {
            AccountId = accountId;
        }
    }
}