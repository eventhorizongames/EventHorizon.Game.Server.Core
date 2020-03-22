namespace EventHorizon.Game.Server.Core.Zone.Model
{
    using System;

    public struct ZoneEntity
    {
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string ServerAddress { get; set; }
        public string Tag { get; set; }
        public DateTime LastPing { get; internal set; }

        public bool IsFound()
        {
            return !string.IsNullOrEmpty(
                Id
            );
        }
    }
}