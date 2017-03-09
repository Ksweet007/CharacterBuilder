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
            //TODO: Take classes from the rulebooks and stick them in cache
            var classList = _classRepository.GetAllClasses();

            return Ok(classList);
        }
    }
}
