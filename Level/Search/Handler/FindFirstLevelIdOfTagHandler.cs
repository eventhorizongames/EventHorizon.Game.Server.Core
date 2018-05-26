using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;
using EventHorizon.Game.Server.Core.Level.Repo;
using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Search.Handler
{
    public class FindFirstLevelIdOfTagHandler : IRequestHandler<FindFirstLevelIdOfTagEvent, string>
    {
        private readonly ILevelRepository _levelRepository;

        public FindFirstLevelIdOfTagHandler(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<string> Handle(FindFirstLevelIdOfTagEvent request, CancellationToken cancellationToken)
        {
            return (await _levelRepository.Find(a => a.Tags.Contains(request.Tag)))?.Id;
        }
    }
}