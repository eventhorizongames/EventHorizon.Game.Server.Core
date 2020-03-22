namespace EventHorizon.Game.Server.Core.Account.Details.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Account.Model;
    using EventHorizon.Game.Server.Core.Account.Repo;
    using MediatR;
    using EventHorizon.Game.Server.Core.Player.Events.Details;
    using EventHorizon.Game.Server.Core.Player.Events.Create;
    using Microsoft.Extensions.Logging;
    using System;
    using EventHorizon.Game.Server.Core.Zone.Exceptions;
    using EventHorizon.Game.Server.Core.Account.Zone;
    using EventHorizon.Game.Server.Core.Player.UpdatePlayer;

    public class GetAccountDetailsHandler : IRequestHandler<GetAccountDetails, AccountDetails>
    {
        private readonly IMediator _mediator;
        private readonly IAccountRepository _accountRepository;

        public GetAccountDetailsHandler(
            ILogger<GetAccountDetailsHandler> logger,
            IMediator mediator,
            IAccountRepository accountRepository
        )
        {
            _mediator = mediator;
            _accountRepository = accountRepository;
        }

        public async Task<AccountDetails> Handle(
            GetAccountDetails request,
            CancellationToken cancellationToken
        )
        {
            return await Map(
                await _accountRepository.FindOrCreate(
                    request.Id
                )
            );
        }

        private async Task<AccountDetails> Map(
            AccountEntity account
        )
        {
            if (string.IsNullOrEmpty(account.Id))
            {
                throw new ArgumentNullException("AccountEntity.Id is null or empty.");
            }
            // Get Player Details
            var playerDetails = await _mediator.Send(
                new QueryPlayerDetailsById(
                    account.Id
                )
            );
            if (playerDetails.IsNew())
            {
                playerDetails = await _mediator.Send(
                    new PlayerCreateNewEvent(
                        account.Id
                    )
                );
            }
            // Get the Player's Zone
            var zone = await _mediator.Send(
                new GetZoneDetailsForAccountId(
                    playerDetails.Id
                )
            );
            if (!zone.IsFound())
            {
                throw new ZoneNotFoundException(
                    playerDetails.Location.CurrentZone,
                    playerDetails.Location.ZoneTag
                );
            }
            await _mediator.Send(
                new UpdatePlayerCommand(
                    playerDetails
                )
            );
            return new AccountDetails
            {
                Id = account.Id,
                Player = playerDetails,
                Zone = zone,
            };
        }
    }
}