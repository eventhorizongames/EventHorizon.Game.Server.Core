using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Search
{
    public class FindFirstLevelIdOfTagEvent : IRequest<string>
    {
        public string Tag { get; set; }
    }
}