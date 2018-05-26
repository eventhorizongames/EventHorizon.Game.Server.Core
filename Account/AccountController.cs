using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Game.Server.Core.Account.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHorizon.Game.Server.Core.Account
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        
        // GET api/Account
        [HttpGet]
        public AccountDetails Get()
        {
            String sub = User.Claims.FirstOrDefault(a => a.Type == "sub").Value;
            return new AccountDetails();
        }
    }
}
