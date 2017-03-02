using System.Web.Http;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/background")]
    public class BackgroundController : ApiController
    {
        private readonly BackgroundRepository _backgroundRepository;

        public BackgroundController()
        {
            _backgroundRepository = new BackgroundRepository();
        }


        [HttpGet]
        [Route("GetAllBackgrounds")]
        public IHttpActionResult GetAllBackgrounds()
        {
            var bgList = _backgroundRepository.GetAllBackgrounds();

            return Ok(bgList);
        }


    }
}