namespace EventHorizon.Game.Server.Core.Zone.Model
{
    public struct ZoneRegistered
    {
        public string Id { get; }
        public bool Success { get; }
        public string ErrorCode { get; }

        public ZoneRegistered(string id)
        {
            this.Success = true;
            this.Id = id;
            this.ErrorCode = null;
        }
        public ZoneRegistered(string id, string errorCode)
        {
            this.Success = false;
            this.Id = id;
            this.ErrorCode = errorCode;
        }
    }
}