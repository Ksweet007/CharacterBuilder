using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Infrastructure.Mappers
{
    public static class CharacterSheetMapper
    {
        public static CharacterSheetDTO MapCharacterSheetDto(CharacterSheet characterSheet)
        {
            var sheetDto = new CharacterSheetDTO
            {
                Id = characterSheet.Id,
                CreatedDate = characterSheet.CreatedDate,
                Class = characterSheet.Class,
                Level = characterSheet.ClassLevel,
                CharacterName = characterSheet.CharacterName,
                PlayerName = characterSheet.PlayerName,
                Alignment = characterSheet.Alignment,
                Background = characterSheet.Background,
                Race = characterSheet.Race,
                Subrace = characterSheet.Subrace,
                ToDo = characterSheet.ToDo,
                IsComplete = characterSheet.IsComplete,
                HpMax = characterSheet.HitPointsMax,
                AbilityScores = new AbilityScores
                {
                    Strength = characterSheet.AbilityScores.Strength,
                    Dexterity = characterSheet.AbilityScores.Dexterity,
                    Constitution = characterSheet.AbilityScores.Constitution,
                    Intelligence = characterSheet.AbilityScores.Intelligence,
                    Wisdom = characterSheet.AbilityScores.Wisdom,
                    Charisma = characterSheet.AbilityScores.Charisma
                }              
            };

            sheetDto.MapAbilityScoreIncreases(characterSheet.AbilityScoreIncreases);
            sheetDto.MapSkillsToSkillProf(characterSheet.Skills);
            sheetDto.MapSkillProficiencies();

            return sheetDto;
        }
    }
}
