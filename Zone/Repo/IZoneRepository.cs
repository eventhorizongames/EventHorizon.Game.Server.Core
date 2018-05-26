using System;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Model;

namespace EventHorizon.Game.Server.Core.Zone.Repo
{
    public interface IZoneRepository
    {
        Task<ZoneEntity> Find(Func<ZoneEntity, bool> predicate);
        Task<ZoneEntity> FindById(string id);
        Task<ZoneEntity> Add(ZoneEntity Zone);
        Task<bool> Remove(ZoneEntity Zone);
    }
}