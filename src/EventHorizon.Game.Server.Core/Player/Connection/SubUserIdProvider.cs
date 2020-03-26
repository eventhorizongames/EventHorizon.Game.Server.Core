using Microsoft.AspNetCore.SignalR;

namespace EventHorizon.Game.Server.Core.Player.Connection
{
    public class SubUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst("sub")?.Value;
        }
    }
}