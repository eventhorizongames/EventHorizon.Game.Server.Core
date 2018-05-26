namespace EventHorizon.Game.Server.Core.Account.Repo
{
    public interface IAccountZoneRepository
    {
        string AccountZone(string accountId);
        void SetAccountZone(string accountId, string ZoneId);
    }
}