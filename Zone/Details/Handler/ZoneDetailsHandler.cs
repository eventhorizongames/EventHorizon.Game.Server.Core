using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Exceptions;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Details.Handler
{
    public class ZoneDetailsHandler : IRequestHandler<ZoneDetailsEvent, ZoneDetails>
    {
        private readonly IZoneRepository _ZoneRepository;

        public ZoneDetailsHandler(IZoneRepository ZoneRepository)
        {
            _ZoneRepository = ZoneRepository;
        }

        public async Task<ZoneDetails> Handle(ZoneDetailsEvent request, CancellationToken cancellationToken)
        {
            try
            {
                return Map(await _ZoneRepository.FindById(request.Id));
            }
            catch (ZoneNotFoundException)
            {
                return ZoneDetails.NULL;
            }
        }

        public ZoneDetails Map(ZoneEntity entity)
        {
            return new ZoneDetails
            {
                Id = entity.Id,
                ServerAddress = entity.ServerAddress,
                Tags = entity.Tags,
            };
        }
    }
}