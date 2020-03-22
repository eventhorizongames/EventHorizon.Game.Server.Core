namespace EventHorizon.Game.Server.Core.Zone.Search
{
    using MediatR;
    
    public class FindFirstZoneIdOfTag : IRequest<string>
    {
        public string Tag { get; }

        public FindFirstZoneIdOfTag(
            string tag
        )
        {
            Tag = tag;
        }
    }
}