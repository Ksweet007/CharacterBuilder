using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Infrastructure.Services
{
    public class CharacterSheetService
    {
        private readonly CharacterSheetRepository _characterSheetRepository;
        private readonly RaceRepository _raceRepository;

        public CharacterSheetService()
        {
            _characterSheetRepository = new CharacterSheetRepository();
            _raceRepository = new RaceRepository();
        }

        public CharacterSheetDTO GetById(int sheetId)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(sheetId);
            var sheetDto = Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetFromDb);
            sheetDto.AllSkills = _characterSheetRepository.ListAllSkills();

            return sheetDto;
        }

        public CharacterSheetDTO CreateNewSheetByUserId(string userId)
        {
            var newSheet = _characterSheetRepository.CreateNewSheet(userId);
            var sheetDto = Mappers.CharacterSheetMapper.MapCharacterSheetDto(newSheet);

            return sheetDto;
        }

        public CharacterSheetDTO UpdateSheet(CharacterSheetDTO sheetToUpdate)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(sheetToUpdate.Id);
            sheetFromDb.AbilityScores.Strength = sheetToUpdate.AbilityScores.Strength;
            sheetFromDb.AbilityScores.Dexterity = sheetToUpdate.AbilityScores.Dexterity;
            sheetFromDb.AbilityScores.Constitution = sheetToUpdate.AbilityScores.Constitution;
            sheetFromDb.AbilityScores.Intelligence = sheetToUpdate.AbilityScores.Intelligence;
            sheetFromDb.AbilityScores.Wisdom = sheetToUpdate.AbilityScores.Wisdom;
            sheetFromDb.AbilityScores.Charisma = sheetToUpdate.AbilityScores.Charisma;
            sheetFromDb.ClassLevel = sheetToUpdate.Level;
            sheetFromDb.PlayerName = sheetToUpdate.PlayerName;
            sheetFromDb.CharacterName = sheetToUpdate.CharacterName;
            sheetFromDb.Alignment = sheetToUpdate.Alignment;
            sheetFromDb.HitPointsMax = sheetToUpdate.HpMax;

            _characterSheetRepository.Update(sheetFromDb);

            return sheetToUpdate;
        }

        public IList<CharacterSheetDTO> ListByUserId(string userId)
        {
            var sheetsFromDb = _characterSheetRepository.GetUserSheets(userId);
            var allSkills = _characterSheetRepository.ListAllSkills();

            var mappedSheets = sheetsFromDb.Select(Mappers.CharacterSheetMapper.MapCharacterSheetDto).ToList();
            foreach (var item in mappedSheets)
            {
                item.AllSkills = allSkills;
            }
            
            return mappedSheets;
        }

        public CharacterSheetDTO SaveRaceSelection(int characterSheetId, int raceId)
        {
            var sheetToSave = _raceRepository.SaveRaceSelection(characterSheetId, raceId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }

        public CharacterSheetDTO SaveSubRaceSelection(int characterSheetId, int subRaceId)
        {
            var sheetToSave = _raceRepository.SaveSubRaceSelection(characterSheetId,subRaceId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }
    }
}
