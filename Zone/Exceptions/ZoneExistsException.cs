using System;

namespace EventHorizon.Game.Server.Core.Zone.Exceptions
{
    public class ZoneExistsException : Exception
    {
        private string ZoneId { get; }
        public ZoneExistsException(string ZoneId)
            : base(string.Format("Zone entity already exists with ZoneId of {0}", ZoneId))
        {
            this.ZoneId = ZoneId;
        }
    }
}