using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EventHorizon.Game.Server.Core.Player.Model
{
    public struct PlayerDetails
    {
        public string Id { get; set; }
        public PositionState Position { get; set; }
        public dynamic Data { get; set; }


        public bool IsNew()
        {
            return this.Id == null;
        }

        public bool IsFound()
        {
            return this.Id != null;
        }
    }
}