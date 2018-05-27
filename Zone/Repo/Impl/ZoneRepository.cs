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
        private static ConcurrentDictionary<string, ZoneEntity> ZONES = new ConcurrentDictionary<string, ZoneEntity>();

        public Task<IEnumerable<ZoneEntity>> GetAll()
        {
            return Task.FromResult(ZONES.Values.AsEnumerable());
        }

        public Task<ZoneEntity> Find(Func<ZoneEntity, bool> predicate)
        {
            try
            {
                return Task.FromResult(ZONES.Values.First(predicate));
            }
            catch (InvalidOperationException ex)
            {
                throw new ZoneNotFoundException(ex);
            }
        }
        public Task<ZoneEntity> FindById(string id)
        {
            var Zone = new ZoneEntity();
            if (!ZONES.TryGetValue(id, out Zone))
            {
                throw new ZoneNotFoundException(id);
            }
            return Task.FromResult(Zone);
        }
        public Task<ZoneEntity> Add(ZoneEntity Zone)
        {
            Zone.Id = Guid.NewGuid().ToString();
            if (!ZONES.TryAdd(Zone.Id, Zone))
            {
                throw new ZoneExistsException(Zone.Id);
            }
            return Task.FromResult(Zone);
        }
        public Task<bool> Remove(ZoneEntity Zone)
        {
            return Task.FromResult(ZONES.TryRemove(Zone.Id, out Zone));
        }
    }
}