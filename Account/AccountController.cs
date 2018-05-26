using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Details;
using EventHorizon.Game.Server.Core.Account.Exceptions;
using EventHorizon.Game.Server.Core.Account.Model;
using EventHorizon.Game.Server.Core.Account.Repo;
using EventHorizon.Game.Server.Core.Level.Exceptions;
using EventHorizon.Game.Server.Core.Level.Repo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHorizon.Game.Server.Core.Account
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/Account
        [HttpGet]
        public async Task<AccountDetails> Get()
        {
            return await _mediator.Send(new AccountDetailsEvent
            {
                Id = User.Claims.FirstOrDefault(a => a.Type == "sub").Value,
            });
        }
    }
}