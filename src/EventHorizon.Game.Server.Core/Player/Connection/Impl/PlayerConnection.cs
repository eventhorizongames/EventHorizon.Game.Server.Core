namespace EventHorizon.Game.Server.Core.Player.Connection.Impl
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.Logging;

    public class PlayerConnection : IPlayerConnection
    {
        private readonly ILogger _logger;
        private readonly HubConnection _connection;

        public PlayerConnection(
            ILogger logger,
            HubConnection connection
        )
        {
            _logger = logger;
            _connection = connection;
        }

        public void OnAction<T>(
            string actionName,
            Action<T> action
        )
        {
            try
            {
                _connection.On<T>(
                    actionName,
                    action
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "On Action failed",
                    ex
                );
                throw;
            }
        }
        public void OnAction(
            string actionName,
            Action action
        )
        {
            try
            {
                _connection.On(
                    actionName,
                    action
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "On Action failed",
                    ex
                );
                throw;
            }
        }

        public async Task<T> SendAction<T>(
            string actionName,
            object[] args
        )
        {
            try
            {
                return await _connection.InvokeCoreAsync<T>(
                    actionName,
                    args
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Send Action failed",
                    ex
                );
                throw;
            }
        }

        public async Task SendAction(
            string actionName,
            object[] args
        )
        {
            try
            {
                await _connection.InvokeCoreAsync(
                    actionName,
                    args
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    "Send Action failed",
                    ex
                );
                throw;
            }
        }
    }
}