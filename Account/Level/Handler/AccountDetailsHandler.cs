using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Exceptions;
using EventHorizon.Game.Server.Core.Account.Level;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Level.Details;
using EventHorizon.Game.Server.Core.Level.Model;
using EventHorizon.Game.Server.Core.Level.Repo;
using EventHorizon.Game.Server.Core.Level.Search;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Level.Handler
{
    public class AccountGetLevelHandler : IRequestHandler<AccountGetLevelEvent, LevelDetails>
    {
        private readonly IMediator _mediator;
        private readonly IAccountLevelRepository _accountLevelRepository;

        public AccountGetLevelHandler(IMediator mediator, IAccountLevelRepository accountLevelRepository)
        {
            _mediator = mediator;
            _accountLevelRepository = accountLevelRepository;
        }

        public async Task<LevelDetails> Handle(AccountGetLevelEvent request, CancellationToken cancellationToken)
        {
            string levelId = string.Empty;
            try
            {
                levelId = _accountLevelRepository.AccountLevel(request.AccountId);
            }
            catch (AccountLevelNotFoundException)
            {
                levelId = await _mediator.Send(new FindFirstLevelIdOfTagEvent
                {
                    Tag = "home",
                });
                _accountLevelRepository.SetAccountLevel(request.AccountId, levelId);
            }
            return await _mediator.Send(new LevelDetailsEvent
            {
                Id = levelId,
            });
        }
    }
}