using System.Net;
using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;
using Microsoft.AspNet.Identity;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/dungeonmaster")]
    public class DungeonMasterController : ApiController
    {
        private readonly DungeonMasterRepository _dmRepo;

        public DungeonMasterController()
        {
            _dmRepo = new DungeonMasterRepository();
        }

        [HttpGet]
        [Route("PlayerInfoCards/{campaignId}")]
        public IHttpActionResult GetPlayerInfoCards(int campaignId)
        {
            if (!User.IsInRole("DungeonMaster")) return StatusCode((HttpStatusCode.Forbidden));

            var userId = User.Identity.GetUserId();
            if (!_dmRepo.DoesUserOwnCampaign(userId, campaignId)) return StatusCode(HttpStatusCode.Forbidden);

            var playerCards = _dmRepo.ListByCampaignId(campaignId);
            return Ok(playerCards);
        }

        [HttpGet]
        [Route("Campaigns")]
        public IHttpActionResult GetCampaigns()
        {

            if (!User.IsInRole("DungeonMaster")) return StatusCode((HttpStatusCode.Forbidden));
            
            var userId = User.Identity.GetUserId();
            var campaigns = _dmRepo.ListByUserId(userId);

            return Ok(campaigns);
        }

        [HttpPost]
        [Route("CreateCampaign/{campaignName}")]
        public IHttpActionResult CreateNewCampaign(string campaignName)
        {
            var userId = User.Identity.GetUserId();
            var newCampaign = _dmRepo.CreateCampaign(userId, campaignName);

            return Ok(newCampaign);
        }

        [HttpDelete]
        [Route("DeleteCampaign/{campaignId}")]
        public IHttpActionResult DeleteCampaign(int campaignId)
        {
            _dmRepo.DeleteCampaignById(campaignId);
            return Ok(campaignId);
        }

    }
}
