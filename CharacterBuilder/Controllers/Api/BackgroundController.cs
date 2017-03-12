using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.Infrastructure.Services;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/background")]
    public class BackgroundController : ApiController
    {
        private readonly BackgroundRepository _backgroundRepository;
        private readonly CharacterSheetService _characterSheetService;

        public BackgroundController()
        {
            _backgroundRepository = new BackgroundRepository();
            _characterSheetService = new CharacterSheetService();
        }


        [HttpGet]
        [Route("GetAllBackgrounds")]
        public IHttpActionResult GetAllBackgrounds()
        {
            var bgList = _backgroundRepository.GetAllBackgrounds();

            return Ok(bgList);
        }

        [HttpPut]
        [Route("SaveBackgroundSelection/{characterSheetId}/{backgroundId}")]
        public IHttpActionResult SaveBackgroundSelection(int characterSheetId, int backgroundId)
        {
            var sheetToUpdate = _characterSheetService.SaveBackgroundSelection(characterSheetId, backgroundId);

            return Ok(sheetToUpdate);
        }


    }
}