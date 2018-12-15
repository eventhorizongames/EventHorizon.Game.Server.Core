
using System;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Bus.Event;
using EventHorizon.Game.Server.Core.Player.Client.SendPlayerProfile;
using EventHorizon.Game.Server.Core.Player.Client.UpdatePlayerProfile;
using EventHorizon.Game.Server.Core.Player.Model;
using EventHorizon.Game.Server.Core.Zone.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Player.Bus
{
    [Authorize]
    public class PlayerBus : Hub<ITypedClientPlayerBus>
    {
        readonly IMediator _mediator;
        public PlayerBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task GetPlayerProfile()
        {
            return _mediator.Publish(
                new SendPlayerProfileToClientEvent
                {
                    PlayerId = GetPlayerId(),
                    ConnectionId = Context.ConnectionId,
                }
            );
        }
        public Task UpdatePlayer(PlayerProfile player)
        {
            return _mediator.Publish(
                new UpdatePlayerProfileFromClientEvent
                {
                    PlayerId = GetPlayerId(),
                    ConnectionId = Context.ConnectionId,
                    Player = player
                }
            );
        }

        private string GetPlayerId()
        {
            return Context.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value ?? String.Empty;
        }
    }
    public interface ITypedClientPlayerBus
    {
        Task PlayerProfile(PlayerClientResponse response);
        Task PlayerUpdated(PlayerClientResponse response);
    }
}