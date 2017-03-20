using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Infrastructure.Mappers
{
    public static class CharacterSheetSkillMapper
    {
        public static IList<SkillListingDTO> MapSkillToDto(IList<Skill> skills)
        {
            return skills.Select(skill => new SkillListingDTO
            {
                Id = skill.Id,
                Name = skill.Name,
                AbilityScoreName = skill.AbilityScore.Name

            }).ToList();

        }
    }
}
