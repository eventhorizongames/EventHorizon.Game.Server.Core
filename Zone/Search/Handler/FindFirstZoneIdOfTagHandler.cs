using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Search.Handler
{
    public class FindFirstZoneIdOfTagHandler : IRequestHandler<FindFirstZoneIdOfTagEvent, string>
    {
        private readonly IZoneRepository _ZoneRepository;

        public FindFirstZoneIdOfTagHandler(IZoneRepository ZoneRepository)
        {
            _ZoneRepository = ZoneRepository;
        }

        public async Task<string> Handle(FindFirstZoneIdOfTagEvent request, CancellationToken cancellationToken)
        {
            return (await _ZoneRepository.Find(a => a.Tag.Equals(request.Tag)))?.Id;
        }
    }
}