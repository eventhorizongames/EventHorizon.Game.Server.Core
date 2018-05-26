using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Model;
using System.Collections.Generic;
using EventHorizon.Game.Server.Core.Level.Exceptions;

namespace EventHorizon.Game.Server.Core.Level.Repo.Impl
{
    public class LevelRepository : ILevelRepository
    {
        private static ConcurrentDictionary<string, LevelEntity> LEVELS = new ConcurrentDictionary<string, LevelEntity>();

        public Task<LevelEntity> Find(Func<LevelEntity, bool> predicate)
        {
            try
            {
                return Task.FromResult(LEVELS.Values.First(predicate));
            }
            catch (InvalidOperationException ex)
            {
                throw new LevelNotFoundException(ex);
            }
        }
        public Task<LevelEntity> FindById(string id)
        {
            var level = new LevelEntity();
            if (!LEVELS.TryGetValue(id, out level))
            {
                throw new LevelNotFoundException(id);
            }
            return Task.FromResult(level);
        }
        public Task<LevelEntity> Add(LevelEntity level)
        {
            level.Id = Guid.NewGuid().ToString();
            if (!LEVELS.TryAdd(level.Id, level))
            {
                throw new LevelExistsException(level.Id);
            }
            return Task.FromResult(level);
        }
        public Task<bool> Remove(LevelEntity level)
        {
            return Task.FromResult(LEVELS.TryRemove(level.Id, out level));
        }
    }
}