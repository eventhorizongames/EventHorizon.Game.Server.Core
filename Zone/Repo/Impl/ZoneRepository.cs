using System;
using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;
using System.Collections.Generic;
using EventHorizon.Game.Server.Core.Zone.Exceptions;

namespace EventHorizon.Game.Server.Core.Zone.Repo.Impl
{
    public class ZoneRepository : IZoneRepository
    {
        private static ConcurrentDictionary<string, ZoneEntity> ZoneS = new ConcurrentDictionary<string, ZoneEntity>();

        public Task<ZoneEntity> Find(Func<ZoneEntity, bool> predicate)
        {
            try
            {
                return Task.FromResult(ZoneS.Values.First(predicate));
            }
            catch (InvalidOperationException ex)
            {
                throw new ZoneNotFoundException(ex);
            }
        }
        public Task<ZoneEntity> FindById(string id)
        {
            var Zone = new ZoneEntity();
            if (!ZoneS.TryGetValue(id, out Zone))
            {
                throw new ZoneNotFoundException(id);
            }
            return Task.FromResult(Zone);
        }
        public Task<ZoneEntity> Add(ZoneEntity Zone)
        {
            Zone.Id = Guid.NewGuid().ToString();
            if (!ZoneS.TryAdd(Zone.Id, Zone))
            {
                throw new ZoneExistsException(Zone.Id);
            }
            return Task.FromResult(Zone);
        }
        public Task<bool> Remove(ZoneEntity Zone)
        {
            return Task.FromResult(ZoneS.TryRemove(Zone.Id, out Zone));
        }
    }
}