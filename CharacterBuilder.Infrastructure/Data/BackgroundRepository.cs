using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class BackgroundRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public BackgroundRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Background> GetAllBackgrounds()
        {
            return
                _db.Backgrounds.Include(b => b.BackgroundCharacteristic.Select(y => y.BackgroundOptions))
                    .Include(s => s.Skills)
                    .Include(l=>l.Languages)
                    .ToList();
        }

        public CharacterSheet SaveBackgroundSelection(int characterSheetId, int backgroundId)
        {
            var backgroundFromDb = _db.Backgrounds
                .Include(s => s.Skills)
                .Single(b => b.Id == backgroundId);

            var sheetFromDb = _db.CharacterSheets
                .Include(t => t.ToDo)
                .Single(s => s.Id == characterSheetId);

            sheetFromDb.Background = backgroundFromDb;
            sheetFromDb.ToDo.HasSelectedBackground = true;
            sheetFromDb.Skills.AddRange(backgroundFromDb.Skills);
            
            Save();

            return sheetFromDb;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
