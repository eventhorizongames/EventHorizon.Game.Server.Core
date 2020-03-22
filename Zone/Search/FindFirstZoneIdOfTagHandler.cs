namespace EventHorizon.Game.Server.Core.Zone.Search.Handler
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Zone.Repo;
    using MediatR;

    public class FindFirstZoneIdOfTagHandler : IRequestHandler<FindFirstZoneIdOfTag, string>
    {
        private readonly IZoneRepository _ZoneRepository;

        public FindFirstZoneIdOfTagHandler(
            IZoneRepository ZoneRepository
        )
        {
            _ZoneRepository = ZoneRepository;
        }

        public async Task<string> Handle(
            FindFirstZoneIdOfTag request,
            CancellationToken cancellationToken
        )
        {
            return (
                await _ZoneRepository.Find(
                    zone => zone.Tag.Equals(
                        request.Tag
                    )
                )
            ).Id;
        }
    }
}