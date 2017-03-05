using System.Web.Mvc;
using CharacterBuilder.ViewModels;

namespace CharacterBuilder.Controllers
{
    [Authorize]
    public class CharacterBuilderController : Controller
    {
        public ActionResult Index()
        {
            var cookie = Request.Cookies["SheetBeingWorked"];
            var sheetId = cookie?.Value;
            
            var model = new IndexViewModel
            {
                UserName = User.Identity.Name,
                SheetId = sheetId
            };

            return View(model);
        }
    }
}
