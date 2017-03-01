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
        private readonly ProficiencyRepository _proficiencyRepository;

        public WeaponService()
        {
            _weaponRepository = new WeaponRepository();
            _proficiencyRepository = new ProficiencyRepository();
        }

        public IList<WeaponCategoryDTO> GetCategoryDTOList()
        {
            var catList = _weaponRepository.GetAllWeaponCategories();
            var profList = _proficiencyRepository.GetWeaponProficiencies();
            
            var returnList = catList.Select(item => new WeaponCategoryDTO
            {
                Id = item.Id, Name = item.Name
            }).ToList();

            foreach (var item in returnList)
            {
                item.ProficiencyId = profList.Single(p => p.Name == item.Name).Id;
            }

            return returnList;
        }

        public void CreateWeapon(WeaponDTO weaponToAdd)
        {
            var newWeapon = new Weapon()
            {
                Id   = 0,
                Name = weaponToAdd.Name,
                Cost = weaponToAdd.Cost,
                DamageDie = weaponToAdd.DamageDie,
                DamageDieCount = weaponToAdd.DamageDieCount,
                Weight = weaponToAdd.Weight,
                WeaponProperties = weaponToAdd.WeaponProperties,
                WeaponCategory = _weaponRepository.GetWeaponCategoryById(weaponToAdd.WeaponCategory.Id),
                ProficiencyId = weaponToAdd.WeaponCategory.ProficiencyId
            };
            
            _weaponRepository.AddWeapon(newWeapon);
        }
    }
}
