using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;

namespace EventHorizon.Game.Server.Core.Account.Repo
{
    public interface IAccountRepository
    {
        Task<AccountEntity> FindUserOrCreateAccount(string id);
    }
}