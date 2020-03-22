namespace EventHorizon.Game.Server.Core.Zone.Details.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using EventHorizon.Game.Server.Core.Zone.Repo;
    using EventHorizon.Game.Server.Core.Zone.Search;
    using MediatR;

    public class FindZoneDetailsBasedOnLocationHandler : IRequestHandler<FindZoneDetailsBasedOnLocation, ZoneDetails>
    {
        private readonly IMediator _mediator;
        private readonly IZoneRepository _zoneRepository;

        public FindZoneDetailsBasedOnLocationHandler(
            IMediator mediator,
            IZoneRepository ZoneRepository
        )
        {
            _mediator = mediator;
            _zoneRepository = ZoneRepository;
        }

        public async Task<ZoneDetails> Handle(
            FindZoneDetailsBasedOnLocation request,
            CancellationToken cancellationToken
        )
        {
            // Lookup Current Zone Exists
            var zone = await _zoneRepository.FindById(
                request.Location.CurrentZone
            );
            // Check if Current Zone Found
            if (!zone.IsFound())
            {
                // Not found, use the Location Tag to find a New Zone
                zone = await _zoneRepository.FindById(
                    await _mediator.Send(
                        new FindFirstZoneIdOfTag(
                            request.Location.ZoneTag
                        )
                    )
                );
            }
            // Map the Entity to The Details
            return new ZoneDetails
            {
                Id = zone.Id,
                ServerAddress = zone.ServerAddress,
                Tag = zone.Tag,
            };
        }
    }
}