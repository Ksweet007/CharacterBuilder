using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{

    [Authorize]
    [RoutePrefix("api/race")]
    public class RaceController : ApiController
    {
        private readonly RaceRepository _raceRepository;

        public RaceController()
        {
            _raceRepository = new RaceRepository();
        }

        [HttpGet]
        [Route("GetAllRaces")]
        public IHttpActionResult GetAllRaces()
        {
            var raceList = _raceRepository.GetAllRaces();

            return Ok(raceList);
        }
        
    }
}