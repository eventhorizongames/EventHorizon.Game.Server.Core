using System;

namespace EventHorizon.Game.Server.Core.Zone.Exceptions
{
    public class ZoneNotFoundException : Exception
    {
        public string ZoneId { get; }
        public string Tag { get; }
        public ZoneNotFoundException(
            string zoneId,
            string tag
        ) : base(
            string.Format(
                "Zone not found with ZoneId of {0} or Tag of {1}", 
                zoneId,
                tag
            )
        )
        {
            ZoneId = zoneId;
            Tag = tag;
        }

        public ZoneNotFoundException(InvalidOperationException ex)
            : base("Zone not found, please see inner exception.", ex)
        {
        }
    }
}