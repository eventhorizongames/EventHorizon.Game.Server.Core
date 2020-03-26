namespace EventHorizon.Game.Server.Core.Account.Model
{
    public struct AccountZoneEntity
    {
        public static readonly AccountZoneEntity NULL = default(AccountZoneEntity);
        
        public string Id { get; set; }
        public string ZoneId { get; set; }
        public string Tag { get; set; }

        public bool IsFound()
        {
            return !this.Equals(NULL);
        }
    }
}