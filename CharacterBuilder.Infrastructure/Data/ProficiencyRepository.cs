using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Enums;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class ProficiencyRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public ProficiencyRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<ProficiencyType> GetAllProficiencyTypes()
        {
            return _db.ProficiencyTypes.ToList();
        }

        public IList<Proficiency> GetAllProficiencies()
        {
            return _db.Proficiencies.ToList();
        }

        public IList<Proficiency> GetAllProficienciesAndIncludeType()
        {
            return _db.Proficiencies.Include(p => p.ProficiencyType).ToList();
        }

        public IList<Proficiency> GetArmorProficiencies()
        {
            return _db.Proficiencies.Where(t => t.ProficiencyType.Name == "Armor").Include(t => t.ProficiencyType).ToList();
        }

        public IList<Proficiency> GetWeaponProficiencies()
        {
            return _db.Proficiencies.Where(t => t.ProficiencyType.Name == "Weapon").Include(t => t.ProficiencyType).ToList();
        }

        public IList<Proficiency> GetProficiencyByProficiencyTypeName(string name)
        {
            return _db.Proficiencies.Where(t => t.ProficiencyType.Name == name).Include(t => t.ProficiencyType).ToList();
        }

        public Proficiency GetProficiencyById(int profId)
        {
            return _db.Proficiencies.Single(t => t.Id == profId);
        }
    }
}
