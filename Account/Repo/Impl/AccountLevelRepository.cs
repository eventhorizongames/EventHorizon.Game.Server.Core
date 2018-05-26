using System.Collections.Concurrent;
using EventHorizon.Game.Server.Core.Account.Exceptions;

namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    public class AccountLevelRepository : IAccountLevelRepository
    {
        private static ConcurrentDictionary<string, string> ACCOUNT_LEVELS = new ConcurrentDictionary<string, string>();
        public string AccountLevel(string accountId)
        {
            string levelId;
            if (!ACCOUNT_LEVELS.TryGetValue(accountId, out levelId))
            {
                throw new AccountLevelNotFoundException(accountId);
            }
            return levelId;
        }

        public void SetAccountLevel(string accountId, string levelId)
        {
            ACCOUNT_LEVELS.AddOrUpdate(accountId, levelId, (key, currentLevelId) => levelId);
        }
    }
}