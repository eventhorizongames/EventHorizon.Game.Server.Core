using EventHorizon.Game.Server.Core.Zone.Model;

namespace EventHorizon.Game.Server.Core.Account.Model
{
    public class AccountDetails
    {
        public string Id { get; set; }
        public ZoneDetails Zone { get; set; }
    }
}