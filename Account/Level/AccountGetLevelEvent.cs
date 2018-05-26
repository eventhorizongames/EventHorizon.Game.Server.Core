using EventHorizon.Game.Server.Core.Level.Model;
using MediatR;

namespace EventHorizon.Game.Server.Core.Account.Level
{
    public struct AccountGetLevelEvent : IRequest<LevelDetails>
    {
        public string AccountId { get; set; }
    }
}