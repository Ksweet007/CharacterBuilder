using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.DTO;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data;

namespace CharacterBuilder.Infrastructure.Services
{
    public class CharacterSheetService
    {
        private readonly CharacterSheetRepository _characterSheetRepository;
        private readonly RaceRepository _raceRepository;
        private readonly ClassRepository _classRepository;
        private readonly BackgroundRepository _backgroundRepository;

        public CharacterSheetService()
        {
            _characterSheetRepository = new CharacterSheetRepository();
            _raceRepository = new RaceRepository();
            _classRepository = new ClassRepository();
            _backgroundRepository = new BackgroundRepository();
        }

        public bool DoesUserOwnSheet(int sheetId, string userId)
        {
            var userSheet = _characterSheetRepository.GetCharacterSheetById(sheetId);

            return userSheet.User.Id == userId;
        }

        public SkillDto GetSkillsBySheetId(int sheetId)
        {
            var skillsFromDb = _characterSheetRepository.GetSkillsBySheetId(sheetId);

            return skillsFromDb;
        }

        public CharacterSheetDTO GetById(int sheetId)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(sheetId);
            var sheetDto = Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetFromDb);

            sheetDto.ProficiencyBonuses = _characterSheetRepository.GetProficiencyBonusesByClassId();

            if (sheetDto.Class?.AbilityScoreIncreaseses == null) return sheetDto;

            if (sheetDto.Class.AbilityScoreIncreaseses.Any(x => x.LevelObtained == sheetDto.Level))
            {
                sheetDto.LevelChecklist.HasAbilityScoreIncrease = true;
            }

            return sheetDto;
        }
        
        public IList<CharacterSheetMinimalDto> ListByUserId(string userId)
        {
            var sheetsFromDb = _characterSheetRepository.GetUserSheets(userId);        

            return sheetsFromDb.Select(sheet => new CharacterSheetMinimalDto
            {
                Id = sheet.Id,
                CharacterName = sheet.CharacterName,
                Class = sheet.Class?.Name ?? "",
                Race = sheet.Race?.Name ?? "",
                Background = sheet.Background?.Name ?? "",
                CreatedDate = sheet.CreatedDate,
                Level = sheet.ClassLevel

            }).ToList();
        }

        public CharacterSheetDTO CreateNewSheetByUserId(string userId)
        {
            var newSheet = _characterSheetRepository.CreateNewSheet(userId);
            var sheetDto = Mappers.CharacterSheetMapper.MapCharacterSheetDto(newSheet);
            
            return sheetDto;
        }

        public CharacterSheetDTO CreateNewSheetWithClass(string userId, int classId)
        {
            var newSheet = _characterSheetRepository.CreateNewSheetWithClass(userId, classId);
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

            sheetFromDb.ToDo.HasSelectedSkills = sheetToUpdate.ToDo.HasSelectedSkills;
            sheetFromDb.ToDo.FirstLevelTasks = sheetToUpdate.ToDo.FirstLevelTasks;
            sheetFromDb.ToDo.MarkFirstLevelTasksComplete();

            var skillsToAdd = sheetToUpdate.SkillsSelected.Select(item => _characterSheetRepository.GetSkillById(item.Id)).ToList();
            sheetFromDb.Skills = skillsToAdd;

            _characterSheetRepository.Update(sheetFromDb);

            return sheetToUpdate;
        }

        public LevelChecklist AddLevelChecklist(int sheetId)
        {
            var sheetFromDb = _characterSheetRepository.GetCharacterSheetById(sheetId);
            sheetFromDb.ClassLevel += 1;
            _characterSheetRepository.Update(sheetFromDb);

            var levelChecklistAdded = _characterSheetRepository.AddLevelChecklist(sheetId);
            levelChecklistAdded.Level = sheetFromDb.ClassLevel;
            levelChecklistAdded.HasAbilityScoreIncrease = sheetFromDb.Class.AbilityScoreIncreaseses.Any(x => x.LevelObtained == sheetFromDb.ClassLevel);

            sheetFromDb.LevelChecklists.Add(levelChecklistAdded);

            return levelChecklistAdded;
        }

        public CharacterSheetDTO SaveBackgroundSelection(int characterSheetId, int backgroundId)
        {
            var sheetToSave = _backgroundRepository.SaveBackgroundSelection(characterSheetId, backgroundId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }

        public CharacterSheetDTO SaveClassSelection(int characterSheetId, int classId)
        {
            var sheetToSave = _classRepository.SaveClassSelection(characterSheetId, classId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }

        public CharacterSheetDTO SaveRaceSelection(int characterSheetId, int raceId)
        {
            var sheetToSave = _raceRepository.SaveRaceSelection(characterSheetId, raceId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }

        public CharacterSheetDTO SaveSubRaceSelection(int characterSheetId, int subRaceId)
        {
            var sheetToSave = _raceRepository.SaveSubRaceSelection(characterSheetId, subRaceId);

            return Mappers.CharacterSheetMapper.MapCharacterSheetDto(sheetToSave);
        }
    }
}
