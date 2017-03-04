using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Core.Model.Character_Sheet;
using CharacterBuilder.Core.Model.User;
using CharacterBuilder.Infrastructure.Data.Contexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CharacterBuilder.Infrastructure.Data
{
    public class CharacterSheetRepository
    {
        private readonly CharacterBuilderDbContext _db;
        private readonly UserManager<ApplicationUser> _manager;

        public CharacterSheetRepository()
        {
            _db = new CharacterBuilderDbContext();
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public CharacterSheet CreateNewSheet(string userId)
        {
            var currentUser = _manager.FindById(userId);
            var sheet = new CharacterSheet {User = currentUser, ToDo = new ToDo()};

            _db.CharacterSheets.Add(sheet);

            Save();

            return sheet;
        }

        public IEnumerable<CharacterSheet> GetUserSheet(string userId)
        {
            var currentUser = _manager.FindById(userId);

            return _db.CharacterSheets.ToList().Where(x => x.User.Id == currentUser.Id);
        }

        public CharacterSheet GetCharacterSheetById(int sheetId)
        {
            return _db.CharacterSheets.Single(s => s.Id == sheetId);           
        }

        public void SaveClassSelection(int classId, int characterSheetId)
        {
            var clsFromDb = _db.Classes.Single(c => c.Id == classId);
            var sheetFromDb = _db.CharacterSheets.Single(s => s.Id == characterSheetId);
            sheetFromDb.Class = clsFromDb;
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
