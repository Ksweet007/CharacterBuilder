using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterBuilder.Core.Enums;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class WeaponRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public WeaponRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Weapon> GetAllWeapons()
        {
            return _db.Weapons.Include(p => p.Proficiency)
                .Include(p => p.WeaponProperties)
                .Include(p => p.WeaponCategory)
                .ToList();
        }

        public Weapon GetWeaponById(int weaponId)
        {
            return _db.Weapons.Single(w => w.Id == weaponId);
        }

        public IList<WeaponProperty> GetAllWeaponProperties()
        {
            return _db.WeaponProperties.ToList();
        }

        public IList<WeaponCategory> GetAllWeaponCategories()
        {
            return _db.WeaponCategories.ToList();
        }

        public WeaponCategory GetWeaponCategoryById(int categoryId)
        {
            return _db.WeaponCategories.Single(c => c.Id == categoryId);
        }

        public void AddWeapon(Weapon weaponToAdd)
        {
            if (weaponToAdd.Id != 0) return;

            _db.Weapons.Add(weaponToAdd);
            Save();
        }

        public void EditWeapon(Weapon weaponToEdit)
        {
            var fromDb = _db.Weapons.Include(p => p.WeaponProperties).Single(w => w.Id == weaponToEdit.Id);

            fromDb.Proficiency = _db.Proficiencies.Single(p => p.Id == weaponToEdit.Proficiency.Id);
            fromDb.Name = weaponToEdit.Name;
            fromDb.Cost = weaponToEdit.Cost;
            fromDb.DamageDieCount = weaponToEdit.DamageDieCount;
            fromDb.DamageDie = weaponToEdit.DamageDie;
            fromDb.WeaponProperties = weaponToEdit.WeaponProperties;

            Save();
        }

        public void DeleteWeaponById(int weaponId)
        {
            var weaponToDelete = _db.Weapons.Single(w => w.Id == weaponId);
            _db.Weapons.Remove(weaponToDelete);

            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
