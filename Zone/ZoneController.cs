using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Zone.Details;
using EventHorizon.Game.Server.Core.Zone.Exceptions;
using EventHorizon.Game.Server.Core.Zone.Model;
using EventHorizon.Game.Server.Core.Zone.Register;
using EventHorizon.Game.Server.Core.Zone.Repo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHorizon.Game.Server.Core.Zone
{
    [Authorize]
    [Route("api/[controller]")]
    public class ZoneController : Controller
    {
        private readonly IMediator _mediator;

        public ZoneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/Zone
        [HttpGet]
        public async Task<IEnumerable<ZoneDetails>> Details()
        {
            return await _mediator.Send(new AllZoneDetailsEvent());
        }

        // GET api/Zone/{id}/Details
        [HttpGet("{id}/Details")]
        public async Task<ZoneDetails> Details([FromRoute] string id)
        {
            return await _mediator.Send(new ZoneDetailsEvent
            {
                Id = id
            });
        }

        // POST api/Zone/Register
        [HttpPost("Register")]
        public async Task<ZoneRegistered> Register([FromBody] ZoneRegister register)
        {
            var Zone = await _mediator.Send(new RegisterZoneEvent
            {
                Zone = new ZoneDetails
                {
                    ServerAddress = register.ServerAddress,
                    Tags = register.Tags,
                }
            });
            return new ZoneRegistered
            {
                Id = Zone.Id,
            };
        }
    }
}