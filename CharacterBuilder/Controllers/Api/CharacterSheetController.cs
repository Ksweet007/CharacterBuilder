using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;
using Microsoft.AspNet.Identity;

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
            var userId = User.Identity.GetUserId();
            var sheets = _characterSheetRepository.GetUserSheet(userId);

            return Ok(sheets);
        }

        [HttpPut]
        [Route("SaveClassSelection/{classId}/{characterSheetId}")]
        public IHttpActionResult SaveClassSelection(int classId, int characterSheetId)
        {
            _characterSheetRepository.SaveClassSelection(classId, characterSheetId);


            return Ok(characterSheetId);   
        }

    }
}