namespace EventHorizon.Game.Server.Core.Account.Zone.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Account.Exceptions;
    using EventHorizon.Game.Server.Core.Account.Model;
    using EventHorizon.Game.Server.Core.Account.Repo;
    using EventHorizon.Game.Server.Core.Zone.Details;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using EventHorizon.Game.Server.Core.Zone.Search;
    using MediatR;
    using EventHorizon.Game.Server.Core.Zone.Exceptions;
    using EventHorizon.Game.Server.Core.Player.Model;

    public class GetZoneDetailsForAccountIdHandler : IRequestHandler<GetZoneDetailsForAccountId, ZoneDetails>
    {
        private readonly IMediator _mediator;
        private readonly IAccountZoneRepository _accountZoneRepository;

        public GetZoneDetailsForAccountIdHandler(
            IMediator mediator,
            IAccountZoneRepository accountZoneRepository
        )
        {
            _mediator = mediator;
            _accountZoneRepository = accountZoneRepository;
        }

        public async Task<ZoneDetails> Handle(
            GetZoneDetailsForAccountId request,
            CancellationToken cancellationToken
        )
        {
            var defaultZoneTag = "home"; // TODO: This should come from settings
            var accountId = request.AccountId;

            // Find Zone Account
            var account = await _accountZoneRepository.FindById(
                accountId
            );

            // Check Account was Found
            if (!account.IsFound())
            {
                // Create New Account
                account = await CreateZoneAccountFromTag(
                    accountId,
                    defaultZoneTag
                );

                // Check Account was Created
                if (!account.IsFound())
                {
                    // Throw Account Zone Not Found
                    throw new AccountZoneNotFoundException(
                        accountId
                    );
                }
            }

            // Check for Zone Details Based On Account Location
            var zoneDetails = await _mediator.Send(
                new FindZoneDetailsBasedOnLocation(
                    new LocationState
                    {
                        CurrentZone = account.ZoneId,
                        ZoneTag = account.Tag,
                    }
                )
            );

            if (!zoneDetails.IsFound())
            {
                throw new ZoneNotFoundException(
                    account.ZoneId,
                    account.Tag
                );
            }

            return zoneDetails;
        }

        private async Task<AccountZoneEntity> CreateZoneAccountFromTag(
            string accountId,
            string tag
        )
        {
            var zoneId = await _mediator.Send(
                new FindFirstZoneIdOfTag(
                    tag
                )
            );
            await _accountZoneRepository.Set(
                new AccountZoneEntity
                {
                    Id = accountId,
                    ZoneId = zoneId,
                    Tag = tag,
                }
            );

            return await _accountZoneRepository.FindById(
                accountId
            );
        }
    }
}