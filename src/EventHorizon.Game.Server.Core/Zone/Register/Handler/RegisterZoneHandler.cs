using System;
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
            return MapToDetails(
                await _ZoneRepository.Add(
                    this.MapToEntity(notification.Zone)
                )
            );
        }
        private ZoneEntity MapToEntity(ZoneDetails details)
        {
            return new ZoneEntity
            {
                Id = details.Id,
                ConnectionId = details.ConnectionId,
                ServerAddress = details.ServerAddress,
                Tag = details.Tag,
                LastPing = DateTime.Now,
                ServiceDetails = details.Details,
            };
        }
        private ZoneDetails MapToDetails(ZoneEntity entity)
        {
            return new ZoneDetails
            {
                Id = entity.Id,
                ConnectionId = entity.ConnectionId,
                ServerAddress = entity.ServerAddress,
                Tag = entity.Tag,
                Details = entity.ServiceDetails,
            };
        }
    }
}