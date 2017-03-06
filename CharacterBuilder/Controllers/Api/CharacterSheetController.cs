using System.Web;
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
        const string Cookie_Name = "SheetBeingWorked";

        public CharacterSheetController()
        {
            _characterSheetRepository = new CharacterSheetRepository();        
        }


        [HttpGet]
        [Route("GetUserSheets")]
        public IHttpActionResult GetUserSheets()
        {
            var userId = User.Identity.GetUserId();
            var sheets = _characterSheetRepository.GetUserSheets(userId);

            return Ok(sheets);
        }

        [HttpPut]
        [Route("SaveClassSelection/{classId}/{characterSheetId}")]
        public IHttpActionResult SaveClassSelection(int classId, int characterSheetId)
        {
            _characterSheetRepository.SaveClassSelection(classId, characterSheetId);
            _characterSheetRepository.ToDoClassSelected(characterSheetId);
            
            return Ok(characterSheetId);   
        }

        [HttpPut]
        [Route("SaveBackgroundSelection/{backgroundId}/{characterSheetId}")]
        public IHttpActionResult SaveBackgroundSelection(int backgroundId, int characterSheetId)
        {
            _characterSheetRepository.SaveBackgroundSelection(backgroundId,characterSheetId);
            _characterSheetRepository.ToDoBackgroundSelected(characterSheetId);

            return Ok(characterSheetId);
        }

        [HttpPut]
        [Route("SaveRaceSelection/{raceId}/{characterSheetId}")]
        public IHttpActionResult SaveRaceId(int raceId, int characterSheetId)
        {
            _characterSheetRepository.SaveRaceSelection(raceId, characterSheetId);
            _characterSheetRepository.ToDoRaceSelected(characterSheetId);

            return Ok(characterSheetId);
        }

        [HttpPost]
        [Route("CreateNewSheet")]
        public IHttpActionResult CreateNewSheet()
        {
            var userId = User.Identity.GetUserId();
            var newSheet = _characterSheetRepository.CreateNewSheet(userId);

            var response = HttpContext.Current.Response;

            //Check if a Cookie already exists. If so remove it, and add a new one so we don't risk collision on what sheet is being worked
            var cookie = new HttpCookie(Cookie_Name, newSheet.Id.ToString());
            response.Cookies.Remove(Cookie_Name);
            response.Cookies.Add(cookie);

            //Each page will need to check if there is a current Sheet being worked or not.
            //Current working cookie will only go away when they delete the sheet, finish the sheet, or create a new sheet
            return Ok(newSheet);
        }

    }
}