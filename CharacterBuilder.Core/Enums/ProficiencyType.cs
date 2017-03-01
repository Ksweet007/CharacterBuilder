using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.Enums
{
    public class ProficiencyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Proficiency> Proficiencies { get; set; }
    }

}
