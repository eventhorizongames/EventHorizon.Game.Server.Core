using System;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;

namespace EventHorizon.Game.Server.Core.Level.Repo
{
    public interface ILevelRepository
    {
        Task<LevelEntity> Find(Func<LevelEntity, bool> predicate);
        Task<LevelEntity> FindById(string id);
        Task<LevelEntity> Add(LevelEntity level);
        Task<bool> Remove(LevelEntity level);
    }
}