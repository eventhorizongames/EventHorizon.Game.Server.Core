using EventHorizon.Game.Server.Core.Level.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Level.Details
{
    public struct LevelDetailsEvent : IRequest<LevelDetails>
    {
         public string Id { get; set; }
    }
}