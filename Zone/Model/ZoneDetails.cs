using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public class ZoneDetails
    {
        public string Id { get; set; }
        public string ServerAddress { get; set; }
        public IList<string> Tags { get; set; }
    }
}