using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Admin.Command.Parser;
using EventHorizon.Game.Server.Core.Admin.Model;
using EventHorizon.Game.Server.Core.Admin.MovePlayer.Event;
using EventHorizon.Game.Server.Core.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventHorizon.Game.Server.Core.Admin.Command.Event.Handler
{
    public class AdminCommandHandler : IRequestHandler<AdminCommandEvent, AdminActionResponse>
    {
        readonly ILogger _logger;
        readonly IMediator _mediator;
        public AdminCommandHandler(ILogger<AdminCommandHandler> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<AdminActionResponse> Handle(AdminCommandEvent request, CancellationToken cancellationToken)
        {
            try
            {
                var command = new AdminCommandParser(request.Command);
                switch (command.Command)
                {
                    case "move-player-by-zone-id":
                    case "mpbzi":
                        await _mediator.Publish(new AdminMovePlayerByZoneIdEvent
                        {
                            PlayerId = command.Parts[0],
                            ZoneId = command.Parts[1]
                        });
                        break;
                    default:
                        throw new BasicCodeException("not_valid_command");

                }
                return new AdminActionResponse { Success = true };
            }
            catch (BasicCodeException ex)
            {
                _logger.LogError("Code based Exception when trying to send admin command.", ex);
                return new AdminActionResponse { Success = false, Reason = ex.Code };
            }
            catch (Exception ex)
            {
                _logger.LogError("General Action when trying to send admin command.", ex);
                return new AdminActionResponse { Success = false, Reason = "general_exception" };
            }
        }
    }
}