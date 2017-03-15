using System.Net;
using System.Web;
using System.Web.Http;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.Infrastructure.Services;
using Microsoft.AspNet.Identity;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/charactersheet")]
    public class CharacterSheetController : ApiController
    {
        private readonly CharacterSheetRepository _characterSheetRepository;
        private readonly CharacterSheetService _characterSheetService;

        const string Cookie_Name = "SheetBeingWorked";

        public CharacterSheetController()
        {
            _characterSheetRepository = new CharacterSheetRepository();        
            _characterSheetService = new CharacterSheetService();            
        }
        
        [HttpGet]
        [Route("GetUserSheets")]
        public IHttpActionResult GetUserSheets()
        {
            var userId = User.Identity.GetUserId();
            var sheets = _characterSheetService.ListByUserId(userId);

            return Ok(sheets);
        }

        [HttpGet]
        [Route("GetSheetById/{sheetId}")]
        public IHttpActionResult GetSheetById(int sheetId)
        {
            var userId = User.Identity.GetUserId();

            var canAccess = _characterSheetService.DoesUserOwnSheet(sheetId, userId);

            if (!canAccess) return StatusCode(HttpStatusCode.Forbidden);

            var returnSheet = _characterSheetService.GetById(sheetId);

            return Ok(returnSheet);
        }

        [HttpPost]
        [Route("CreateNewSheet")]
        public IHttpActionResult CreateNewSheet()
        {
            var userId = User.Identity.GetUserId();
            var newSheet = _characterSheetService.CreateNewSheetByUserId(userId);

            var response = HttpContext.Current.Response;
            var cookie = new HttpCookie(Cookie_Name, newSheet.Id.ToString());
            response.Cookies.Remove(Cookie_Name);
            response.Cookies.Add(cookie);
            
            return Ok(newSheet);
        }

        [HttpPost]
        [Route("CreateNewSheetWithClass/{classId}")]
        public IHttpActionResult CreateNewSheetWithClass(int classId)
        {
            var userId = User.Identity.GetUserId();
            var newSheet = _characterSheetService.CreateNewSheetWithClass(userId, classId);

            var response = HttpContext.Current.Response;
            var cookie = new HttpCookie(Cookie_Name, newSheet.Id.ToString());
            response.Cookies.Remove(Cookie_Name);
            response.Cookies.Add(cookie);

            return Ok(newSheet);
        }

        [HttpPut]
        [Route("EditSheet/")]
        public IHttpActionResult EditSheet([FromBody] CharacterSheetDTO sheetToEdit)
        {
            _characterSheetService.UpdateSheet(sheetToEdit);

            return Ok(sheetToEdit);
        }

        [HttpPost]
        [Route("AddLevelChecklist/{sheetId}")]
        public IHttpActionResult AddLevelChecklist(int sheetId)
        {
            var lvlChecklist = _characterSheetService.AddLevelChecklist(sheetId);

            return Ok(lvlChecklist);
        }

        [HttpDelete]
        [Route("DeleteSheet/{sheetId}")]
        public IHttpActionResult DeleteSheet(int sheetId)
        {
            var cookie = HttpContext.Current.Request.Cookies[Cookie_Name];
            if (cookie != null)
            {
                if (cookie.Value == sheetId.ToString())
                {
                    HttpContext.Current.Response.Cookies.Remove(Cookie_Name);
                }                
            }

            _characterSheetRepository.DeleteSheetAndToDoList(sheetId);

            return Ok(sheetId);
        }

    }
}