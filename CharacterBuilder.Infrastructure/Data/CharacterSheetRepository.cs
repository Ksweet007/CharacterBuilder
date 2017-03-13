using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                User = currentUser
            };

            _db.CharacterSheets.Add(sheet);

            Save();

            return sheet;
        }

        public CharacterSheet CreateNewSheetWithClass(string userId, int classId)
        {
            var currentUser = _manager.FindById(userId);
            var clsFromDb = _db.Classes
                .Include(s => s.Skills)
                .Include(f => f.Features)
                .Single(c => c.Id == classId);


            var sheet = new CharacterSheet
            {
                User = currentUser,
                ToDo = new ToDo
                {
                    HasSelectedClass = true
                },
                CreatedDate = DateTime.UtcNow,
                Class = clsFromDb,
            };

            _db.CharacterSheets.Add(sheet);

            Save();

            return sheet;
        }

        public IList<CharacterSheet> GetUserSheets(string userId)
        {
            var currentUser = _manager.FindById(userId);

            return _db.CharacterSheets
                .Include(t => t.ToDo)
                .Include(c => c.Class.Skills.Select(a => a.AbilityScore))
                .Include(f => f.Class.Features)
                .Include(b => b.Background)
                .Include(r => r.Race)
                .Include(sr => sr.Subrace)
                .Include(i => i.AbilityScoreIncreases.Select(a => a.AbilityScore))
                .Where(x => x.User.Id == currentUser.Id).ToList();

        }

        public CharacterSheet GetCharacterSheetById(int sheetId)
        {
            return _db.CharacterSheets.Include(t => t.ToDo)
                .Include(c => c.Class)
                .Include(b => b.Background)
                .Include(r => r.Race)
                .Include(sr => sr.Subrace)
                .Include(a => a.AbilityScoreIncreases.Select(y => y.AbilityScore))
                .Single(s => s.Id == sheetId);
        }

        public IList<Skill> ListAllSkills()
        {
            return _db.Skills.ToList();
        }

        public void DeleteSheetAndToDoList(int characterSheetId)
        {
            var sheetFromDb = _db.CharacterSheets
                .Include(t => t.ToDo)
                .Single(s => s.Id == characterSheetId);

            var toDoToDelete = sheetFromDb.ToDo;

            _db.CharacterSheets.Remove(sheetFromDb);
            _db.ToDos.Remove(toDoToDelete);

            Save();
        }

        public void Update<T>(T entity) where T : class
        {
            _db.Entry(entity).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
