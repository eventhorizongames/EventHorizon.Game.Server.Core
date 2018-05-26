using System.Collections.Concurrent;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Level.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Repo.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private static ConcurrentDictionary<string, AccountEntity> ACCOUNTS = new ConcurrentDictionary<string, AccountEntity>();
        private readonly IMediator _mediator;
        public AccountRepository(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<AccountEntity> FindUserOrCreateAccount(string id)
        {
            AccountEntity account;
            if (!ACCOUNTS.TryGetValue(id, out account))
            {
                account = new AccountEntity
                {
                    Id = id,
                    CurrentLevel = await _mediator.Send(new FindFirstLevelIdOfTagEvent
                    {
                        Tag = "home",
                    }),
                };
            }
            return account;
        }
    }
}