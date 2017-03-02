using System.Collections.Generic;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cost { get; set; }
        public int DamageDie { get; set; }
        public int DamageDieCount { get; set; }
        public string Weight { get; set; }
        public int ProficiencyId { get; set; }
        public Proficiency Proficiency { get; set; }
        public string ProficiencyName => Proficiency?.Name;
        public virtual IList<WeaponProperty> WeaponProperties { get; set; }
        public virtual WeaponCategory WeaponCategory { get; set; }
    }
}
