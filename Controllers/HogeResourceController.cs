using System;
using System.Collections.Generic;
using System.Linq;
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
            return "PROTECTED RESOURCE";
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