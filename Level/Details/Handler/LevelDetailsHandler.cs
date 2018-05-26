using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;
using EventHorizon.Game.Server.Core.Level.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Details.Handler
{
    public class LevelDetailsHandler : IRequestHandler<LevelDetailsEvent, LevelDetails>
    {
        private readonly ILevelRepository _levelRepository;

        public LevelDetailsHandler(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<LevelDetails> Handle(LevelDetailsEvent request, CancellationToken cancellationToken)
        {
            return Map(await _levelRepository.FindById(request.Id));
        }

        public LevelDetails Map(LevelEntity entity)
        {
            return new LevelDetails
            {
                Id = entity.Id,
                ServerAddress = entity.ServerAddress,
            };
        }
    }
}