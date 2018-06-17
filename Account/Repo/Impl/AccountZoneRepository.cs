using System.Collections.Concurrent;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Exceptions;
using EventHorizon.Game.Server.Core.Account.Model;

namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    public class AccountZoneRepository : IAccountZoneRepository
    {
        private static ConcurrentDictionary<string, AccountZoneEntity> ACCOUNT_ZONES = new ConcurrentDictionary<string, AccountZoneEntity>();
        public Task<AccountZoneEntity> FindById(string accountId)
        {
            var accountZone = default(AccountZoneEntity);
            if (!ACCOUNT_ZONES.TryGetValue(accountId, out accountZone))
            {
                throw new AccountZoneNotFoundException(accountId);
            }
            return Task.FromResult(accountZone);
        }

        public Task<bool> Contains(string accountId)
        {
            return Task.FromResult(
                ACCOUNT_ZONES.ContainsKey(accountId)
            );
        }

        public Task Set(AccountZoneEntity entity)
        {
            ACCOUNT_ZONES.AddOrUpdate(entity.Id, entity, (key, currentEntity) => entity);
            return Task.CompletedTask;
        }
    }
}