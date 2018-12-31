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
            var zone = new ZoneEntity();
            if (!ZONES.TryGetValue(id, out zone))
            {
                throw new ZoneNotFoundException(id);
            }
            return Task.FromResult(zone);
        }
        public Task<ZoneEntity> Add(ZoneEntity zone)
        {
            if (String.IsNullOrEmpty(zone.Id))
            {
                throw new ZoneIdInvalidException();
            }
            if (!ZONES.TryAdd(zone.Id, zone))
            {
                throw new ZoneExistsException(zone.Id);
            }
            return Task.FromResult(zone);
        }

        public async Task<ZoneEntity> Update(ZoneEntity zone)
        {
            if (!ZONES.TryUpdate(zone.Id, zone, await FindById(zone.Id)))
            {
                throw new ZoneNotFoundException(zone.Id);
            }
            return zone;
        }

        public Task<bool> Remove(ZoneEntity zone)
        {
            return Task.FromResult(ZONES.TryRemove(zone.Id, out zone));
        }
    }
}