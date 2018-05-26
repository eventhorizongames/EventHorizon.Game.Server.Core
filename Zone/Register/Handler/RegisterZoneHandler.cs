using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Register.Handler
{
    public class RegisterZoneHandler : IRequestHandler<RegisterZoneEvent, ZoneDetails>
    {
        private readonly IZoneRepository _ZoneRepository;

        public RegisterZoneHandler(IZoneRepository ZoneRepository)
        {
            _ZoneRepository = ZoneRepository;
        }

        public async Task<ZoneDetails> Handle(RegisterZoneEvent notification, CancellationToken cancellationToken)
        {
            return MapToDetails(await _ZoneRepository.Add(this.MapToEntity(notification.Zone)));
        }
        private ZoneEntity MapToEntity(ZoneDetails details)
        {
            return new ZoneEntity
            {
                Id = details.Id,
                ServerAddress = details.ServerAddress,
                Tags = details.Tags,
            };
        }
        private ZoneDetails MapToDetails(ZoneEntity entity)
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