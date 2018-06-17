using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;

namespace EventHorizon.Game.Server.Core.Account.Repo
{
    public interface IAccountZoneRepository
    {
        Task<AccountZoneEntity> FindById(string accountId);
        Task<bool> Contains(string accountId);
        Task Set(AccountZoneEntity entity);
    }
}