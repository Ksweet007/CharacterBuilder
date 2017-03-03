using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/charactersheet")]
    public class CharacterSheetController : ApiController
    {
        private readonly CharacterSheetRepository _characterSheetRepository;

        public CharacterSheetController()
        {
            _characterSheetRepository = new CharacterSheetRepository();
        }


        [HttpGet]
        [Route("GetUserSheets")]
        public IHttpActionResult GetUserSheets()
        {
            var userName = User.Identity.Name;
            var sheets = _characterSheetRepository.GetCharacterSheetByUserName(userName);
            return Ok(sheets);
        }

    }
}