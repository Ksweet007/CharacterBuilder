using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class ClassRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public ClassRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Class> GetAllClasses()
        {
            return _db.Classes.ToList();

        }

        public CharacterSheet SaveClassSelection(int characterSheetId, int classId)
        {
            var clsFromDb = _db.Classes
                .Include(s => s.Skills)
                .Include(f => f.Features)
                .Single(c => c.Id == classId);

            var sheetFromDb = _db.CharacterSheets
                .Include(t => t.ToDo)
                .Single(s => s.Id == characterSheetId);

            sheetFromDb.Class = clsFromDb;
            sheetFromDb.ToDo.HasSelectedClass = true;
            
            Save();

            return sheetFromDb;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
    }
}
