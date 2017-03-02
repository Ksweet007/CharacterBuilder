using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class CharacterSheetRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public CharacterSheetRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public CharacterSheet GetCharacterSheetById(int sheetId)
        {
            return _db.CharacterSheets.Single(s => s.Id == sheetId);           
        }

        public void CreateCharacterSheet(CharacterSheet sheetToCreate)
        {
            _db.CharacterSheets.Add(sheetToCreate);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }


    }
}
