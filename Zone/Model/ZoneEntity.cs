using System;
using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public class ZoneEntity
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string ServerAddress { get; set; } 
        public string Tag { get; set; }
        public DateTime LastPing { get; internal set; }
    }
}