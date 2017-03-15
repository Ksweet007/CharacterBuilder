using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class SkillDto
    {
        public IList<Skill> AllSkills { get; set; }
        public IList<int> Skills { get; set; } 
        public IList<Skill> SkillProficiencies { get; set; } 
        public int SkillPickCount { get; set; }
    }
}
