using System;
using System.Collections.Generic;
using System.Linq;
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
            return UpdateCharacterSheet(charactersheetId, s => s.ToDo.HasSelectedRace = true);

        }

        public CharacterSheet SaveRaceSelection(int charactersheetId, int raceId)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(charactersheetId);
            var raceFromDb = _raceRepository.GetRaceById(raceId);

            var increasesFromRace = raceFromDb.AbilityScoreIncreases.Select(item => sheetFromDb.AbilityScores.Where(x => x.Name == item.AbilityScore.Name)).ToList();
            foreach(var item in increasesFromRace)
            {
                item.GetEnumerator().Current.Value += raceFromDb.AbilityScoreIncreases.Single( s => s.AbilityScore.Name == item.GetEnumerator().Current.Name).IncreaseValue;
            }

            return UpdateCharacterSheet(charactersheetId, s =>
            {
                s.Race = raceFromDb;
                

            });
        }

        //public void SaveRaceSelection(int raceId, int characterSheetId)
        //{
        //    var raceFromDb = _db.Races.Include(a => a.AbilityScoreIncreases.Select(y => y.AbilityScore)).Single(r => r.Id == raceId);
        //    var sheetFromDb = _db.CharacterSheets.Include(a => a.AbilityScores).Single(s => s.Id == characterSheetId);
        //    sheetFromDb.Race = raceFromDb;

        //    foreach (var item in raceFromDb.AbilityScoreIncreases)
        //    {
        //        var scoreToIncrease = sheetFromDb.AbilityScores.Single(x => x.Name == item.AbilityScore.Name);
        //        scoreToIncrease.Value += item.IncreaseValue;
        //        sheetFromDb.AbilityScores.Add(new AbilityScoreSheetValue { Name = item.AbilityScore.Name, Value = item.IncreaseValue });
        //    }

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
