using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/proficiency")]
    public class ProficiencyController : ApiController
    {
        private readonly ProficiencyRepository _proficiencyRepository;

        public ProficiencyController()
        {
            _proficiencyRepository = new ProficiencyRepository();
        }

        [HttpGet]
        [Route("GetAllProficiencies")]
        public IHttpActionResult GetAllProficiencies()
        {
            var profList = _proficiencyRepository.GetAllProficienciesAndIncludeType();

            return Ok(profList);
        }

        [HttpGet]
        [Route("GetAllProficiencyTypes")]
        public IHttpActionResult GetAllProficiencyTypes()
        {
            var profTypeList = _proficiencyRepository.GetAllProficiencyTypes();

            return Ok(profTypeList);
        }

    }
}