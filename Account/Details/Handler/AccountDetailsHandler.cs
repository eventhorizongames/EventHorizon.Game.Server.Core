using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Zone;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Zone.Details;
using EventHorizon.Game.Server.Core.Zone.Search;
using MediatR;
using EventHorizon.Game.Server.Core.Player.Events.Details;
using EventHorizon.Game.Server.Core.Player.Events.Create;
using EventHorizon.Game.Server.Core.Player.Events.Zone;

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
            // Get Player Details
            var playerDetails = await _mediator.Send(new PlayerGetDetailsEvent
            {
                Id = entity.Id
            });
            if (playerDetails.IsNew())
            {
                playerDetails = await _mediator.Send(new PlayerCreateNewEvent
                {
                    Id = entity.Id
                });
            }
            // Get the Player's Zone
            var zone = await _mediator.Send(new ZoneDetailsEvent
            {
                Id = playerDetails.Position.CurrentZone,
            });
            if (!zone.IsFound())
            {
                playerDetails = await _mediator.Send(new PlayerFindNewZoneEvent
                {
                    Player = playerDetails
                });
            }
            return new AccountDetails
            {
                Id = entity.Id,
                Player = playerDetails,
                Zone = await _mediator.Send(new ZoneDetailsEvent
                {
                    Id = playerDetails.Position.CurrentZone,
                })
            };
        }
    }
}