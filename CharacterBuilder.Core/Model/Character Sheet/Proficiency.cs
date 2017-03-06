using System.Collections.Generic;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Proficiency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProficiencyType ProficiencyType { get; set; }
        public IList<Weapon> Weapons { get; set; }
        public IList<Armor> Armors { get; set; }
        public IList<Class> Classes { get; set; }
        
    }
}
