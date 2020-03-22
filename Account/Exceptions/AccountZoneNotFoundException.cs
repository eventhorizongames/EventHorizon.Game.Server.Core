namespace EventHorizon.Game.Server.Core.Account.Exceptions
{
    using System;

    public class AccountZoneNotFoundException : Exception
    {
        public string AccountId { get; }

        public AccountZoneNotFoundException(string accountId)
            : base(string.Format("Zone Account was Not Found for AccountId '{0}'", accountId))
        {
            AccountId = accountId;
        }
    }
}