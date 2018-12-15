namespace EventHorizon.Game.Server.Core.Player.Model
{
    public struct PlayerClientResponse
    {
        public bool Success { get; set; }
        public PlayerProfile Player { get; set; }
        public string MessageCode { get; set; }
    }
}