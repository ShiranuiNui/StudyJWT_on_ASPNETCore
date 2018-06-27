using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StudyJWT_on_ASPNETCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HogeResourceController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var username = this.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            return $"Hi {username}. YOU ARE ACCESSING PROTECTED RESOURCE";
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<string> Post()
        {
            return Accepted();
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]
        public ActionResult<string> Put()
        {
            return Accepted();
        }

    }
}