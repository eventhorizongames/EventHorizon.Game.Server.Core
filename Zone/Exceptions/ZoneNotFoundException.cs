using System;

namespace EventHorizon.Game.Server.Core.Zone.Exceptions
{
    public class ZoneNotFoundException : Exception
    {
        public string ZoneId { get; }
        public ZoneNotFoundException(string ZoneId)
            : base(string.Format("Zone not found with ZoneId of {0}", ZoneId))
        {
            this.ZoneId = ZoneId;
        }

        public ZoneNotFoundException(InvalidOperationException ex)
            : base("Zone not found, please see inner exception.", ex)
        {
        }
    }
}