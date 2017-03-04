using System.Web.Mvc;
using CharacterBuilder.ViewModels;

namespace CharacterBuilder.Controllers
{
    [Authorize]
    public class CharacterBuilderController : Controller
    {
        public ActionResult Index()
        {
            //TODO: This needs to give the most recent sheetId, then it needs to be changed when the user selects one on their main page
            var model = new IndexViewModel
            {
                SheetId = 5,
                UserName = User.Identity.Name
            };

            return View(model);
        }
    }
}
