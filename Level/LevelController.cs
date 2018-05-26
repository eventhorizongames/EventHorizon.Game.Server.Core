using System;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Level.Details;
using EventHorizon.Game.Server.Core.Level.Exceptions;
using EventHorizon.Game.Server.Core.Level.Model;
using EventHorizon.Game.Server.Core.Level.Register;
using EventHorizon.Game.Server.Core.Level.Repo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHorizon.Game.Server.Core.Level
{
    [Authorize]
    [Route("api/[controller]")]
    public class LevelController : Controller
    {
        private readonly IMediator _mediator;

        public LevelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/Level/{id}/Details
        [HttpGet("{id}/Details")]
        public async Task<LevelDetails> Details([FromRoute] string id)
        {
            return await _mediator.Send(new LevelDetailsEvent
            {
                Id = id
            });
        }

        // POST api/Level/Register
        [HttpPost("Register")]
        public async Task<LevelRegistered> Register([FromBody] LevelRegister register)
        {
            var level = await _mediator.Send(new RegisterLevelEvent
            {
                Level = new LevelDetails
                {
                    ServerAddress = register.ServerAddress,
                    Tags = register.Tags,
                }
            });
            return new LevelRegistered
            {
                Id = level.Id,
            };
        }
    }
}