using System;
using CharacterBuilder.Core.Model;
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

        public CharacterSheet SetToDoSubRaceSelectedDone(int charactersheetId, int subRaceId)
        {
            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedSubRace = true);
        }

        public CharacterSheet SetToDoRaceSelectedDone(int charactersheetId, int subRaceId)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(charactersheetId);
            sheetFromDb.ToDo.HasSelectedRace = true;
            sheetFromDb.Update();

            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedRace = true);
        }

        public CharacterSheet SaveRaceSelection(int charactersheetId, int raceId)
        {
            var raceFromDb = _raceRepository.GetRaceById(raceId);
            
            return UpdateCharacterSheet(charactersheetId, s =>
            {
                s.Race = raceFromDb;
                s.AbilityScoreIncreases.AddRange(raceFromDb.AbilityScoreIncreases);
            });
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
