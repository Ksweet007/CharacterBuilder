using System.Collections.Generic;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AbilityScore AbilityScore { get; set; }
        public IList<Class> Classes { get; set; }
        public IList<CharacterSheet> CharacterSheets { get; set; }
    }
}
