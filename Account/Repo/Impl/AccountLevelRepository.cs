using System.Collections.Concurrent;
using EventHorizon.Game.Server.Core.Account.Exceptions;

namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    public class AccountZoneRepository : IAccountZoneRepository
    {
        private static ConcurrentDictionary<string, string> ACCOUNT_ZoneS = new ConcurrentDictionary<string, string>();
        public string AccountZone(string accountId)
        {
            string ZoneId;
            if (!ACCOUNT_ZoneS.TryGetValue(accountId, out ZoneId))
            {
                throw new AccountZoneNotFoundException(accountId);
            }
            return ZoneId;
        }

        public void SetAccountZone(string accountId, string ZoneId)
        {
            ACCOUNT_ZoneS.AddOrUpdate(accountId, ZoneId, (key, currentZoneId) => ZoneId);
        }
    }
}