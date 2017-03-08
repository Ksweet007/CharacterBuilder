using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CharacterBuilder.Core.Model;
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
        private readonly BaseRepository 

        public CharacterSheetRepository()
        {
            _db = new CharacterBuilderDbContext();
            _manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public CharacterSheet CreateNewSheet(string userId)
        {
            var currentUser = _manager.FindById(userId);
            var sheet = new CharacterSheet
            {
                User = currentUser, ToDo = new ToDo(), CreatedDate = DateTime.UtcNow                
            };

            _db.CharacterSheets.Add(sheet);

            Save();

            return sheet;
        }

        public IList<CharacterSheet> GetUserSheets(string userId)
        {
            var currentUser = _manager.FindById(userId);

            return _db.CharacterSheets.Include(c => c.Class)
                .Include(b=>b.Background)
                .Include(r=>r.Race.AbilityScoreIncreases.Select(a=>a.AbilityScore))
                .Where(x => x.User.Id == currentUser.Id).ToList();
        }

        public CharacterSheet GetCharacterSheetById(int sheetId)
        {
            return _db.CharacterSheets.Include(t=>t.ToDo)
                .Include(t=>t.ToDo)
                .Single(s => s.Id == sheetId);
        }

        public void SaveClassSelection(int classId, int characterSheetId)
        {
            var clsFromDb = _db.Classes.Single(c => c.Id == classId);
            var sheetFromDb = _db.CharacterSheets.Single(s => s.Id == characterSheetId);
            sheetFromDb.Class = clsFromDb;

            Save();
        }

        public void SaveBackgroundSelection(int backgroundId, int characterSheetId)
        {

            var bgFromDb = _db.Backgrounds.Single(b => b.Id == backgroundId);
            var sheetFromDb = _db.CharacterSheets.Single(s => s.Id == characterSheetId);
            sheetFromDb.Background = bgFromDb;

            Save();
        }

        //public void SaveRaceSelection(int raceId, int characterSheetId)
        //{
        //    var raceFromDb = _db.Races.Include(a=>a.AbilityScoreIncreases.Select(y=>y.AbilityScore)).Single(r => r.Id == raceId);
        //    //var sheetFromDb = _db.CharacterSheets.Include(a=>a.AbilityScores).Single(s => s.Id == characterSheetId);
        //    var sheetFromDb = _db.CharacterSheets.Include(a => a.AbilityScores).Single(s => s.Id == characterSheetId);
        //    sheetFromDb.Race = raceFromDb;

        //    foreach (var item in raceFromDb.AbilityScoreIncreases)
        //    {
        //        var scoreToIncrease = sheetFromDb.AbilityScores.Single(x => x.Name == item.AbilityScore.Name);
        //        scoreToIncrease.Value += item.IncreaseValue;
        //        sheetFromDb.AbilityScores.Add(new AbilityScoreSheetValue {Name = item.AbilityScore.Name, Value = item.IncreaseValue});
        //    }

        //    Save();
        //}

        public void SaveSubRaceSelection(int subRaceId, int characterSheetId)
        {


            Save();
        }

        public void ToDoClassSelected (int characterSheetId)
        {
            var sheetFromDb = _db.CharacterSheets.Include(t=>t.ToDo).Single(s => s.Id == characterSheetId);
            sheetFromDb.ToDo.HasSelectedClass = true;

            Save();
        }

        public void ToDoBackgroundSelected(int characterSheetId)
        {
            var sheetFromDb = _db.CharacterSheets.Include(t => t.ToDo).Single(s => s.Id == characterSheetId);
            sheetFromDb.ToDo.HasSelectedBackground = true;

            Save();
        }


        public void ToDoSubRaceSelected(int characterSheetId)
        {
            var sheetFromDb = _db.CharacterSheets.Include(t => t.ToDo).Single(s => s.Id == characterSheetId);
            sheetFromDb.ToDo.HasSelectedSubRace = true;

            Save();
        }
        
        public void DeleteSheetAndToDoList(int characterSheetId)
        {
            var sheetFromDb = _db.CharacterSheets
                .Include(t=>t.ToDo)
                .Single(s => s.Id == characterSheetId);

            var toDoToDelete = sheetFromDb.ToDo;

            _db.CharacterSheets.Remove(sheetFromDb);
            _db.ToDos.Remove(toDoToDelete);

            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
