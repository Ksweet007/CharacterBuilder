using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.Enums
{
    public class WeaponCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Weapon> Weapons { get; set; }
    }
}
