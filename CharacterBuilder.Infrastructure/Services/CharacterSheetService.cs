using System;
using System.Collections.Generic;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Infrastructure.Services
{
    public class CharacterSheetService
    {
        private readonly CharacterSheetRepository _characterSheetRepository;
        private readonly RaceRepository _raceRepository;
        private readonly BaseEfRepository _repository; 

        public CharacterSheetService()
        {
            _characterSheetRepository = new CharacterSheetRepository();
            _raceRepository = new RaceRepository();
            _repository = new BaseEfRepository();
        }

        //public Race Get(int id) => _repository.GetById<Race>(id);
        public CharacterSheetDTO GetSheetInfoAndMapToDTO(int id)
        {
            var sheetFromDB = _repository.GetById<CharacterSheet>(id);

            var sheetDTO = new CharacterSheetDTO
            {
                Id = sheetFromDB.Id,
                CreatedDate = sheetFromDB.CreatedDate,
                Class = sheetFromDB.Class,
                Level = sheetFromDB.ClassLevel,
                CharacterName = sheetFromDB.CharacterName,
                PlayerName = sheetFromDB.PlayerName,
                Alignment = sheetFromDB.Alignment,
                Background = sheetFromDB.Background,
                Race = sheetFromDB.Race,
                ToDo = sheetFromDB.ToDo,
                IsComplete = sheetFromDB.IsComplete,
                HpMax = sheetFromDB.HitPointsMax,
                Strength = sheetFromDB.AbilityScores.Strength,
                Dexterity = sheetFromDB.AbilityScores.Dexterity,
                Constitution = sheetFromDB.AbilityScores.Constitution,
                Intelligence = sheetFromDB.AbilityScores.Intelligence,
                Wisdom = sheetFromDB.AbilityScores.Wisdom,
                Charisma = sheetFromDB.AbilityScores.Charisma                
            };
            //TO DO: Add SUBRACE
            var increaseList = new List<AbilityScoreIncrease>();
            var raceIncreases = _raceRepository.GetByRaceId(sheetFromDB.Race.Id);
            

            return sheetDTO;
        }

        public CharacterSheet SetToDoSubRaceSelectedDone(int charactersheetId, int subRaceId)
        {
            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedSubRace = true);
        }

        public CharacterSheet SetToDoRaceSelectedDone(int charactersheetId, int subRaceId)
        {
            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedRace = true);
        }

        public CharacterSheet SaveRaceSelection(int charactersheetId, int raceId)
        {
            var raceFromDb = _raceRepository.GetRaceById(raceId);
            
            return UpdateCharacterSheet(charactersheetId, s => s.Race = raceFromDb);
        }

        private CharacterSheet UpdateCharacterSheet(int characterSheetId, Action<CharacterSheet> characterSheetModifications )
        {
            var sheetfromDb = _characterSheetRepository.GetCharacterSheetById(characterSheetId);

            characterSheetModifications(sheetfromDb);

            _characterSheetRepository.Save();

            return sheetfromDb;
        }

    }
}
