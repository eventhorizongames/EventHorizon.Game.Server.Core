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

namespace EventHorizon.Game.Server.Core.Account.Zone.Handler
{
    public class AccountGetZoneHandler : IRequestHandler<AccountGetZoneEvent, ZoneDetails>
    {
        private readonly IMediator _mediator;
        private readonly IAccountZoneRepository _accountZoneRepository;

        public AccountGetZoneHandler(IMediator mediator, IAccountZoneRepository accountZoneRepository)
        {
            _mediator = mediator;
            _accountZoneRepository = accountZoneRepository;
        }

        public async Task<ZoneDetails> Handle(AccountGetZoneEvent request, CancellationToken cancellationToken)
        {
            string ZoneId = string.Empty;
            try
            {
                ZoneId = _accountZoneRepository.AccountZone(request.AccountId);
            }
            catch (AccountZoneNotFoundException)
            {
                ZoneId = await _mediator.Send(new FindFirstZoneIdOfTagEvent
                {
                    Tag = "home",
                });
                _accountZoneRepository.SetAccountZone(request.AccountId, ZoneId);
            }
            return await _mediator.Send(new ZoneDetailsEvent
            {
                Id = ZoneId,
            });
        }
    }
}