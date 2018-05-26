using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public class ZoneRegister
    {
        public string ServerAddress { get; set; }
        public IList<string> Tags { get; set; }
    }
}