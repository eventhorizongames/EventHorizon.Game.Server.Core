using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Zone;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Zone.Details;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Details.Handler
{
    public class AccountDetailsHandler : IRequestHandler<AccountDetailsEvent, AccountDetails>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public AccountDetailsHandler(IMediator mediator, IAccountRepository accountRepository)
        {
            _mediator = mediator;
            _accountRepository = accountRepository;
        }

        public async Task<AccountDetails> Handle(AccountDetailsEvent request, CancellationToken cancellationToken)
        {
            return await Map(await _accountRepository.FindOrCreate(request.Id));
        }

        private async Task<AccountDetails> Map(AccountEntity entity)
        {
            return new AccountDetails
            {
                Id = entity.Id,
                Zone = await _mediator.Send(new AccountGetZoneEvent
                {
                    AccountId = entity.Id,
                }),
            };
        }
    }
}