using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class WeaponDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int DamageDie { get; set; }
        public int DamageDieCount { get; set; }
        public string Weight { get; set; }
        public IList<WeaponProperty> WeaponProperties { get; set; }
        public WeaponCategoryDTO WeaponCategory { get; set; }
    }
}
