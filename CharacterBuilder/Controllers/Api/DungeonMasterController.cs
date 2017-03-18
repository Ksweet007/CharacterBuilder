using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/dungeonmaster")]
    public class DungeonMasterController : ApiController
    {
        [HttpGet]
        [Route("PlayerInfoCards")]
        public IHttpActionResult GetPlayerInfoCards(int campaignId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("Campaigns")]
        public IHttpActionResult GetCampaigns(int userId)
        {
            return Ok();
        }

    }
}
