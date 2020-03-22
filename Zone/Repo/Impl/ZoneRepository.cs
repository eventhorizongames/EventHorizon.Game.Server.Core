namespace EventHorizon.Game.Server.Core.Zone.Repo.Impl
{
    using System;
    using System.Linq;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using EventHorizon.Game.Server.Core.Zone.Model;
    using System.Collections.Generic;
    using EventHorizon.Game.Server.Core.Zone.Exceptions;

    public class ZoneRepository : IZoneRepository
    {
        private static ConcurrentDictionary<string, ZoneEntity> ZONES = new ConcurrentDictionary<string, ZoneEntity>();

        public Task<IEnumerable<ZoneEntity>> GetAll()
        {
            return Task.FromResult(
                ZONES.Values.AsEnumerable()
            );
        }

        public Task<ZoneEntity> Find(
            Func<ZoneEntity, bool> predicate
        )
        {
            return Task.FromResult(
                ZONES.Values.FirstOrDefault(
                    predicate
                )
            );
        }

        public Task<ZoneEntity> FindById(
            string id
        )
        {
            id = id ?? string.Empty;
            if (ZONES.TryGetValue(
                id,
                out var zone
            ))
            {
                return Task.FromResult(
                    zone
                );
            }

            return Task.FromResult(
                new ZoneEntity()
            );
        }
        public Task<ZoneEntity> Add(
            ZoneEntity zone
        )
        {
            CheckForNull(
                zone
            );
            if (ZONES.ContainsKey(
                zone.Id
            ))
            {
                throw new ZoneExistsException(
                    zone.Id
                );
            }
            return Task.FromResult(
                zone
            );
        }

        public async Task<ZoneEntity> Update(
            ZoneEntity zone
        )
        {
            CheckForNull(
                zone
            );
            if (!ZONES.TryUpdate(
                zone.Id,
                zone,
                await FindById(zone.Id)
            ))
            {
                throw new ZoneNotFoundException(
                    zone.Id,
                    string.Empty
                );
            }
            return zone;
        }

        public Task<bool> Remove(
            ZoneEntity zone
        )
        {
            var zoneId = zone.Id ?? string.Empty;
            return Task.FromResult(
                ZONES.TryRemove(
                    zoneId,
                    out _
                )
            );
        }

        private static void CheckForNull(ZoneEntity zone)
        {
            if (!zone.IsFound())
            {
                throw new ArgumentNullException(
                    nameof(zone.Id)
                );
            }
        }
    }
}