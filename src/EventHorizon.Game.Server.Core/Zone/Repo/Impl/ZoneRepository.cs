namespace EventHorizon.Game.Server.Core.Zone.Repo.Impl;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Exceptions;
using EventHorizon.Game.Server.Core.Zone.Model;

public class ZoneRepository : IZoneRepository
{
    private readonly ConcurrentDictionary<string, ZoneEntity> _zones = new();

    public int Count => _zones.Count;

    public Task<IEnumerable<ZoneEntity>> GetAll()
    {
        return Task.FromResult(_zones.Values.AsEnumerable());
    }

    public Task<ZoneEntity> Find(Func<ZoneEntity, bool> predicate)
    {
        return Task.FromResult(_zones.Values.FirstOrDefault(predicate));
    }

    public Task<ZoneEntity> FindById(string id)
    {
        id ??= string.Empty;
        if (_zones.TryGetValue(id, out var zone))
        {
            return Task.FromResult(zone);
        }

        return Task.FromResult(new ZoneEntity());
    }

    public Task<ZoneEntity> Add(ZoneEntity zone)
    {
        CheckForNull(zone);
        if (!_zones.TryAdd(zone.Id, zone))
        {
            throw new ZoneExistsException(zone.Id);
        }
        return Task.FromResult(zone);
    }

    public async Task<ZoneEntity> Update(ZoneEntity zone)
    {
        CheckForNull(zone);
        if (!_zones.TryUpdate(zone.Id, zone, await FindById(zone.Id)))
        {
            throw new ZoneNotFoundException(zone.Id, string.Empty);
        }
        return zone;
    }

    public Task<bool> Remove(ZoneEntity zone)
    {
        var zoneId = zone.Id ?? string.Empty;
        return Task.FromResult(_zones.TryRemove(zoneId, out _));
    }

    private static void CheckForNull(ZoneEntity zone)
    {
        if (!zone.IsFound())
        {
            throw new ArgumentNullException(nameof(zone.Id));
        }
    }
}
