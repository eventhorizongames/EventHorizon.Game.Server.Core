namespace EventHorizon.Game.Server.Core.Account.Repo
{
    public interface IAccountLevelRepository
    {
        string AccountLevel(string accountId);
        void SetAccountLevel(string accountId, string levelId);
    }
}