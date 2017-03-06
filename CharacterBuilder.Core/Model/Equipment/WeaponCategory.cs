using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class WeaponCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Weapon> Weapons { get; set; }
    }
}
