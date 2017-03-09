using System;
using System.Collections.Generic;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Infrastructure.Services
{
    public class CharacterSheetService
    {
        private readonly BaseEfRepository _repository;
        private readonly CharacterSheetRepository _characterSheetRepository;
        private readonly RaceRepository _raceRepository;

        public CharacterSheetService()
        {
            _repository = new BaseEfRepository();
            _characterSheetRepository = new CharacterSheetRepository();
            _raceRepository = new RaceRepository();
        }

        public CharacterSheetDTO GetSheetInfoAndMapToDTO(int id)
        {
            var sheetFromDB = _characterSheetRepository.GetCharacterSheetById(id);

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

            sheetDTO.MapAbilityScoreIncreases(sheetFromDB.AbilityScoreIncreases);

            return sheetDTO;
        }

        public CharacterSheet SetToDoSubRaceSelectedDone(int charactersheetId)
        {
            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedSubRace = true);
        }
        
        //public CharacterSheet SaveRaceSelection(int charactersheetId, int raceId)
        //{

        //}

        private CharacterSheet UpdateCharacterSheet(int characterSheetId, Action<CharacterSheet> characterSheetModifications )
        {
            var sheetfromDb = _characterSheetRepository.GetCharacterSheetById(characterSheetId);

            characterSheetModifications(sheetfromDb);

            _characterSheetRepository.Save();

            return sheetfromDb;
        }

    }
}
