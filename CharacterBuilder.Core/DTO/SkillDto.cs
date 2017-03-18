using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class SkillDto
    {
        public IList<SkillListingDTO> AllSkills { get; set; }
        public IList<int> SelectedSkills { get; set; } 
        public IList<Skill> SkillProficiencies { get; set; } 
        public IList<Skill> BackgroundSkills { get; set; }
        public int SkillPickCount { get; set; }
    }
}
