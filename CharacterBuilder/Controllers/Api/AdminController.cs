using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CharacterBuilder.Controllers.Api
{
    public class AdminController : ApiController
    {
        [HttpGet]
        [Route("api/IsAdmin")]
        public IHttpActionResult IsUserAdmin()
        {
            var isAdmin = User.IsInRole("Admin");

            return Ok(isAdmin);
        }
    }
}
