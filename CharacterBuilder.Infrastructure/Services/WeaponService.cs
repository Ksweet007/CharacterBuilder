using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Infrastructure.Services
{
    public class WeaponService
    {
        private readonly WeaponRepository _weaponRepository;

        public WeaponService()
        {
            _weaponRepository = new WeaponRepository();
        }

        public void CreateWeapon(Weapon weaponToAdd)
        {
            var newWeapon = new Weapon()
            {
                Id   = 0,
                Name = weaponToAdd.Name,
                Cost = weaponToAdd.Cost,
                DamageDie = weaponToAdd.DamageDie,
                DamageDieCount = weaponToAdd.DamageDieCount,
                Weight = weaponToAdd.Weight,
                ProficiencyId = weaponToAdd.Proficiency.Id,
            };
            var weaponCategoryPicked = _weaponRepository.GetWeaponCategoryById(weaponToAdd.WeaponCategory.Id);
            newWeapon.WeaponCategory = weaponCategoryPicked;

            var propList = weaponToAdd.WeaponProperties.Select(item => _weaponRepository.GetWeaponPropertyById(item.Id)).ToList();
            newWeapon.WeaponProperties = propList;

            _weaponRepository.AddWeapon(newWeapon);
        }
    }
}
