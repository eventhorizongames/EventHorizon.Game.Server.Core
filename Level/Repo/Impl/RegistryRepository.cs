using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;
using System.Collections.Generic;

namespace EventHorizon.Game.Server.Core.Level.Repo.Impl
{
    public class LevelRepository : ILevelRepository
    {
        private static ConcurrentDictionary<string, LevelEntity> LEVELS = new ConcurrentDictionary<string, LevelEntity>();

        public Task<LevelEntity> Find(Func<LevelEntity, bool> predicate)
        {
            return Task.FromResult(LEVELS.Values.FirstOrDefault(predicate));
        }
        public Task<LevelEntity> FindById(string id)
        {
            var level = new LevelEntity();
            LEVELS.TryGetValue(id, out level);
            return Task.FromResult(level);
        }
        public Task<LevelEntity> Add(LevelEntity level)
        {
            level.Id = Guid.NewGuid().ToString();
            LEVELS.TryAdd(level.Id, level);
            return Task.FromResult(level);
        }
        public Task<bool> Remove(LevelEntity level)
        {
            return Task.FromResult(LEVELS.TryRemove(level.Id, out level));
        }
    }
}