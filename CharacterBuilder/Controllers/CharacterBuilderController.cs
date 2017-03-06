using System;
using System.Web.Mvc;
using CharacterBuilder.Infrastructure.Data;
using CharacterBuilder.ViewModels;

namespace CharacterBuilder.Controllers
{
    [Authorize]
    public class CharacterBuilderController : Controller
    {
        private readonly CharacterSheetRepository _characterSheetRepository;

        public CharacterBuilderController()
        {
            _characterSheetRepository = new CharacterSheetRepository();
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel{UserName = User.Identity.Name};

            var cookie = Request.Cookies["SheetBeingWorked"];
            if (cookie == null) return View(model);

            var sheetId = cookie.Value;
            model.SheetId = sheetId;

            var sheetInProgress = _characterSheetRepository.GetCharacterSheetById(Convert.ToInt32(sheetId));
            model.HasSelectedClass = sheetInProgress.ToDo.HasSelectedClass;
            model.HasSelectedRace = sheetInProgress.ToDo.HasSelectedRace;

            return View(model);
        }
    }
}
