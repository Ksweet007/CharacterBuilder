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

            return sheetDto;
        }

        public CharacterSheetDTO CreateNewSheetByUserId(string userId)
        {
            var newSheet = _characterSheetRepository.CreateNewSheet(userId);
            var sheetDto = Mappers.CharacterSheetMapper.MapCharacterSheetDto(newSheet);

            return sheetDto;
        }

        public IList<CharacterSheetDTO> ListByUserId(string userId)
        {
            var sheetsFromDb = _characterSheetRepository.GetUserSheets(userId);

            return sheetsFromDb.Select(Mappers.CharacterSheetMapper.MapCharacterSheetDto).ToList();
        }

        public CharacterSheetDTO SaveRaceSelection(int characterSheetId, int raceId)
        {
            var sheetToSave = _raceRepository.SaveRaceSelection(characterSheetId, raceId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }  
    }
}
