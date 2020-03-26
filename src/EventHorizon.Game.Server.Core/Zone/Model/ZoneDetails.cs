using System;
using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public class ZoneDetails
    {
        public readonly static ZoneDetails NULL = new ZoneDetails
        {
            Id = null,
        };
        public string Id { get; internal set; }
        public string ServerAddress { get; internal set; }
        public string ConnectionId { get; internal set; }
        public string Tag { get; internal set; }
        public DateTime LastPing { get; internal set; }

        public bool IsFound()
        {
            return Id != null;
        }
    }
}