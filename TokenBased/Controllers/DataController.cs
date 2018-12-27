using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace TokenBased.Controllers
{
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/all")]
        public IHttpActionResult Get()
        {
            return Ok("Now server time is"+DateTime.Now.ToString());
        }
        [Authorize]
        [HttpGet]
        [Route("api/data/auth/user")]
        public IHttpActionResult GetAuth()
        {
            var Identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello "+Identity.Name);
        }
        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/data/auth/admin")]
        public IHttpActionResult GetAdmin()
        {
            var Identity = (ClaimsIdentity)User.Identity;
            var role = Identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
            return Ok("Hello "+Identity.Name + " Roles : "+ string.Join(",",role.ToList()));
        }
    }
}
