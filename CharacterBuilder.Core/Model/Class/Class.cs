using System.Collections.Generic;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Class : BaseEntity
    {         
        public string Name { get; set; }
        public string Description { get; set; }
        public string Primaryability { get; set; }
        public string Hitdie { get; set; }
        public IList<Feature> Features { get; set; }
        public IList<Skill> Skills { get; set; }
        public int SkillPickCount { get; set; }        
        public IList<Proficiency> Proficiencies { get; set; }
        public IList<ProficiencyType> ProficiencyTypes { get; set; }        
        public IList<AbilityScoreImprovement> AbilityScoreIncreaseses { get; set; }         
    }
}
