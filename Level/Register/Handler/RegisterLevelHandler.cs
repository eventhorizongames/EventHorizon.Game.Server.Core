using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;
using EventHorizon.Game.Server.Core.Level.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Register.Handler
{
    public class RegisterLevelHandler : IRequestHandler<RegisterLevelEvent, LevelDetails>
    {
        private readonly ILevelRepository _levelRepository;

        public RegisterLevelHandler(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<LevelDetails> Handle(RegisterLevelEvent notification, CancellationToken cancellationToken)
        {
            return MapToDetails(await _levelRepository.Add(this.MapToEntity(notification.Level)));
        }
        private LevelEntity MapToEntity(LevelDetails details)
        {
            return new LevelEntity
            {
                Id = details.Id,
                ServerAddress = details.ServerAddress,
                Tags = details.Tags,
            };
        }
        private LevelDetails MapToDetails(LevelEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            return new LevelDetails
            {
                Id = entity.Id,
                ServerAddress = entity.ServerAddress,
                Tags = entity.Tags,
            };
        }
    }
}