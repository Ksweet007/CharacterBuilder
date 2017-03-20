using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.Infrastructure.Services;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/class")]
    public class ClassController : ApiController
    {
        private readonly ClassRepository _classRepository;
        private readonly CharacterSheetService _characterSheetService;

        public ClassController()
        {
            _classRepository = new ClassRepository();
            _characterSheetService = new CharacterSheetService();
        }

        [HttpGet]
        [Route("GetAllClasses")]
        public IHttpActionResult GetAllClasses()
        {            
            var classList = _classRepository.GetAllClasses();

            return Ok(classList);
        }

        [HttpPut]
        [Route("SaveClassSelection/{characterSheetId}/{classId}")]
        public IHttpActionResult SaveClassSelection(int characterSheetId, int classId)
        {
            var characterSheet = _characterSheetService.SaveClassSelection(characterSheetId, classId);

            return Ok(characterSheet);
        }
    }
}
