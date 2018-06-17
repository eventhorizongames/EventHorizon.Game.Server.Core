using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Exceptions;
using EventHorizon.Game.Server.Core.Account.Zone;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Zone.Details;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;
using EventHorizon.Game.Server.Core.Zone.Exceptions;

namespace EventHorizon.Game.Server.Core.Account.Zone.Handler
{
    public class AccountGetZoneHandler : IRequestHandler<AccountGetZoneEvent, ZoneDetails>
    {
        private readonly static int MAX_RUNS = 3;
        private readonly IMediator _mediator;
        private readonly IAccountZoneRepository _accountZoneRepository;

        public AccountGetZoneHandler(IMediator mediator, IAccountZoneRepository accountZoneRepository)
        {
            _mediator = mediator;
            _accountZoneRepository = accountZoneRepository;
        }

        public async Task<ZoneDetails> Handle(AccountGetZoneEvent request, CancellationToken cancellationToken)
        {
            return await GetZoneDetails(request.AccountId);
        }

        public async Task<ZoneDetails> GetZoneDetails(string accountId, int run = 0)
        {
            run++;
            if (run > MAX_RUNS)
            {
                throw new AccountZoneNotFoundException(accountId);
            }
            var account = default(AccountZoneEntity);
            try
            {
                account = await _accountZoneRepository.FindById(accountId);
                return await _mediator.Send(new ZoneDetailsEvent
                {
                    Id = account.ZoneId
                });
            }
            catch (AccountZoneNotFoundException)
            {
                await CreateZoneAccountFromTag(accountId, "home");
                return await GetZoneDetails(accountId, run);
            }
            catch (ZoneNotFoundException)
            {
                if (!AccountZoneEntity.NULL.Equals(account))
                {
                    await CreateZoneAccountFromTag(accountId, account.Tag);
                    return await GetZoneDetails(accountId, run);
                }
                throw new AccountZoneNotFoundException(accountId);
            }
        }
        private async Task<string> CreateZoneAccountFromTag(string accountId, string tag)
        {
            var zoneId = await _mediator.Send(new FindFirstZoneIdOfTagEvent
            {
                Tag = tag,
            });
            await _accountZoneRepository.Set(new AccountZoneEntity
            {
                Id = accountId,
                ZoneId = zoneId,
                Tag = tag,
            });

            return zoneId;
        }
    }
}