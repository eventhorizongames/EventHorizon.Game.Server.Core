namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Account.Model;
    using MediatR;

    public class AccountRepository : IAccountRepository
    {
        private static ConcurrentDictionary<string, AccountEntity> ACCOUNTS = new ConcurrentDictionary<string, AccountEntity>();

        private readonly IMediator _mediator;

        public AccountRepository(
            IMediator mediator
        )
        {
            _mediator = mediator;
        }

        public Task<AccountEntity> FindOrCreate(
            string id
        )
        {
            if (id == null)
            {
                throw new ArgumentNullException(
                    nameof(id)
                );
            }
            AccountEntity account;
            if (!ACCOUNTS.TryGetValue(
                id, out account
            ))
            {
                account = new AccountEntity
                {
                    Id = id,
                };
                ACCOUNTS.TryAdd(
                    account.Id,
                    account
                );
            }
            return Task.FromResult(
                account
            );
        }
    }
}