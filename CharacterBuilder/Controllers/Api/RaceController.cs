using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.Infrastructure.Services;

namespace CharacterBuilder.Controllers.Api
{

    [Authorize]
    [RoutePrefix("api/race")]
    public class RaceController : ApiController
    {
        private readonly RaceRepository _raceRepository;
        private readonly CharacterSheetService _characterSheetService;

        public RaceController()
        {
            _raceRepository = new RaceRepository();
            _characterSheetService = new CharacterSheetService();
        }

        [HttpGet]
        [Route("GetAllRaces")]
        public IHttpActionResult GetAllRaces()
        {
            var raceList = _raceRepository.GetAllRaces();

            return Ok(raceList);
        }

        [HttpPut]
        [Route("SaveRaceSelection/{characterSheetId}/{raceId}")]
        public IHttpActionResult SaveRaceSelection(int characterSheetId,int raceId )
        {
            var characterSheet = _characterSheetService.SaveRaceSelection(characterSheetId, raceId);
            _characterSheetService.SetToDoRaceSelectedDone(characterSheetId);

            return Ok(characterSheet);
        }

        [HttpPut]
        [Route("SaveSubRaceSelection/{characterSheetId}/{subRaceId}")]
        public IHttpActionResult SaveSubRaceSelection(int characterSheetId, int subRaceId)
        {
            var characterSheet = _characterSheetService.SetToDoSubRaceSelectedDone(characterSheetId);

            return Ok(characterSheet);
        }

    }
}