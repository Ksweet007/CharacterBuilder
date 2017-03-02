using System.Web.Mvc;
using CharacterBuilder.ViewModels;

namespace CharacterBuilder.Controllers
{
    [Authorize]
    public class CharacterBuilderController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel
            {
                SheetId = 1,
                UserName = User.Identity.Name
            };

            return View(model);
        }
    }
}
