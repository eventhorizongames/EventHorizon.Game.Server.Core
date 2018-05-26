using EventHorizon.Game.Server.Core.Level.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Register
{
    public struct RegisterLevelEvent : IRequest<LevelDetails>
    {
        public LevelDetails Level { get; set; }
    }
}