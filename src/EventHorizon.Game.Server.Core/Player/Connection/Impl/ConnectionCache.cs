namespace EventHorizon.Game.Server.Core.Player.Connection.Impl
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Connections.Client;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class ConnectionCache : IConnectionCache, IDisposable
    {
        readonly ILogger _logger;

        private HubConnection _connection;

        public ConnectionCache(
            ILogger<ConnectionCache> logger
        )
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _connection?.DisposeAsync().GetAwaiter().GetResult();
        }

        public async Task<HubConnection> GetConnection(
            string url, 
            Action<HttpConnectionOptions> configureHttpConnection
        )
        {
            if (_connection == null)
            {
                try
                {
                    _connection = new HubConnectionBuilder()
                        .AddNewtonsoftJsonProtocol()
                        .WithUrl(url, configureHttpConnection)
                        .Build();
                    _connection.Closed += (ex) =>
                    {
                        _connection = null;
                        return Task.FromResult(0);
                    };
                    await _connection.StartAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error connecting", ex);
                    _connection = null;
                    throw ex;
                }
            }
            return _connection;
        }
    }
}