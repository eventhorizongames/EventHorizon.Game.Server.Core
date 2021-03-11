using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public class ZoneRegistrationDetails
    {
        public string ServerAddress { get; set; }
        public string Tag { get; set; }
        public ServiceDetails Details { get; set; }
    }
}