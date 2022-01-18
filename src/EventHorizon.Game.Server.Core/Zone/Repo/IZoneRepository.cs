namespace EventHorizon.Game.Server.Core.Zone.Repo;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EventHorizon.Game.Server.Core.Zone.Model;

public interface IZoneRepository
{
    int Count { get; }
    Task<IEnumerable<ZoneEntity>> GetAll();
    Task<ZoneEntity> Find(Func<ZoneEntity, bool> predicate);
    Task<ZoneEntity> FindById(string id);
    Task<ZoneEntity> Add(ZoneEntity Zone);
    Task<ZoneEntity> Update(ZoneEntity Zone);
    Task<bool> Remove(ZoneEntity Zone);
}
