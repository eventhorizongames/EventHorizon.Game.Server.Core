namespace EventHorizon.Game.Server.Core.Logging.ExternalHub
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Logging;

    [Authorize]
    public class ClientLoggingHub
        : Hub
    {
        private readonly ILogger _logger;

        public ClientLoggingHub(
            ILoggerFactory loggerFactory
        )
        {
            _logger = loggerFactory.CreateLogger(
                "ClientLogger"
            );
        }

        public bool LogMessage(
            PlatformLogMessage message
        )
        {
            using var scope = _logger.BeginScope(
                message.Args
            );
            var levelFound = Enum.TryParse<LogLevel>(
                message.Level, 
                out var level
            );
            _logger.Log(
                levelFound ? level : LogLevel.Information,
                message.Message
            );
            return true;
        }
    }

    public class PlatformLogMessage
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public Dictionary<string, object> Args { get;set; }
    }
}
