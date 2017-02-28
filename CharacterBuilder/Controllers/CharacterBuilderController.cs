using System.Web.Mvc;

namespace CharacterBuilder.Controllers
{
    [System.Web.Mvc.Authorize]
    public class CharacterBuilderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
