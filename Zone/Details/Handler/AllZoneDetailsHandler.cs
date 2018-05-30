using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Details.Handler
{
    public class AllZoneDetailsHandler : IRequestHandler<AllZoneDetailsEvent, IEnumerable<ZoneDetails>>
    {
        private readonly IZoneRepository _zoneRepository;

        public AllZoneDetailsHandler(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task<IEnumerable<ZoneDetails>> Handle(AllZoneDetailsEvent request, CancellationToken cancellationToken)
        {
            return (await _zoneRepository.GetAll()).Select(a => Map(a));
        }

        public ZoneDetails Map(ZoneEntity entity)
        {
            return new ZoneDetails
            {
                Id = entity.Id,
                ServerAddress = entity.ServerAddress,
                Tags = entity.Tags,
                LastPing = entity.LastPing,
            };
        }
    }
}