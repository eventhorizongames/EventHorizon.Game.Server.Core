using MediatR;

namespace EventHorizon.Game.Server.Core.Zone.Search
{
    public class FindFirstZoneIdOfTagEvent : IRequest<string>
    {
        public string Tag { get; set; }
    }
}