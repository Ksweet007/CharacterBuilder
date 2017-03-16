using System.Linq;
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
                Class = characterSheet.Class ?? new Class(),
                Level = characterSheet.ClassLevel,
                CharacterName = characterSheet.CharacterName,
                PlayerName = characterSheet.PlayerName,
                Alignment = characterSheet.Alignment,
                Background = characterSheet.Background ?? new Background(),
                Race = characterSheet.Race ?? new Race(),
                Subrace = characterSheet.Subrace ?? new Subrace(),
                ToDo = characterSheet.ToDo,                
                HpMax = characterSheet.HitPointsMax,
                LevelChecklist = characterSheet.LevelChecklists.SingleOrDefault(c => c.Level == characterSheet.ClassLevel)?? new LevelChecklist(),
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
            sheetDto.MarkLevelChecklistComplete();

            return sheetDto;
        }
    }
}
