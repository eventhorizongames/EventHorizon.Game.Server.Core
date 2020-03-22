namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Account.Model;

    public class AccountZoneRepository : IAccountZoneRepository
    {
        private static ConcurrentDictionary<string, AccountZoneEntity> ACCOUNT_ZONES = new ConcurrentDictionary<string, AccountZoneEntity>();

        public Task<AccountZoneEntity> FindById(
            string accountId
        )
        {
            accountId = accountId ?? string.Empty;
            if (ACCOUNT_ZONES.TryGetValue(
                accountId,
                out var accountZone
            ))
            {
                return Task.FromResult(
                    accountZone
                );
            }
            return Task.FromResult(
                AccountZoneEntity.NULL
            );
        }

        public Task Set(
            AccountZoneEntity entity
        )
        {
            if (string.IsNullOrEmpty(
                entity.Id
            ))
            {
                throw new ArgumentNullException(
                    nameof(entity.Id)
                );
            }
            ACCOUNT_ZONES.AddOrUpdate(
                entity.Id,
                entity,
                (_, __) => entity
            );
            return Task.CompletedTask;
        }
    }
}