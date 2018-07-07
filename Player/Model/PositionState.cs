using System.Numerics;

namespace EventHorizon.Game.Server.Core.Player.Model
{
    public class PositionState
    {
        public string CurrentZone { get; set; }
        public string ZoneTag { get; set; }
        public Vector3 Position { get; set; }
    }
}