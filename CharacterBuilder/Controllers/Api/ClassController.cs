using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/class")]
    public class ClassController : ApiController
    {
        private readonly ClassRepository _classRepository;

        public ClassController()
        {
            _classRepository = new ClassRepository();
        }

        [HttpGet]
        [Route("GetAllClasses")]
        public IHttpActionResult GetAllClasses()
        {
            var classList = _classRepository.GetAllClasses();

            return Ok(classList);
        }
    }
}
