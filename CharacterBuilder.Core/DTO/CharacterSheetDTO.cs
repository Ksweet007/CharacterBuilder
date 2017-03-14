using System;
using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class CharacterSheetDTO : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public Class Class { get; set; } = new Class();
        public int Level { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Alignment { get; set; }
        public Background Background { get; set; }
        public Race Race { get; set; }
        public Subrace Subrace { get; set; }
        public IList<int> Skills { get; set; } = new List<int>();
        public IList<Skill> AllSkills { get; set; }
        public IList<Skill> SkillProficiencies { get; set; }  = new List<Skill>();
        public ToDo ToDo { get; set; } = new ToDo();        
        public LevelChecklist LevelChecklist { get; set; } //Only load one per level, once saved, grab new one from DB
        public int HpMax { get; set; }        
        public AbilityScores AbilityScores { get; set; }
        public IList<ScoreIncrease> AbilityScoreIncreases { get; set; } = new List<ScoreIncrease>();
        public bool LevelChecklistComplete { get; set; }

        public void MarkLevelChecklistComplete()
        {
            if (LevelChecklist == null)
            {
                LevelChecklistComplete = false;
                return;
            }

            if (LevelChecklist.HasAbilityScoreIncrease)
            {
                LevelChecklistComplete = LevelChecklist.HasIncreasedAbilityScores && LevelChecklist.HasIncreasedHp;
            }
            else
            {
                LevelChecklistComplete = LevelChecklist.HasIncreasedHp;
            }
        }

        public void MapAbilityScoreIncreases(IList<AbilityScoreIncrease>increases )
        {
            foreach(var item in increases)
            {
                AbilityScoreIncreases.Add( new ScoreIncrease
                {
                    Id = item.Id,
                    IncreaseAmount = item.IncreaseValue,
                    Name = item.AbilityScore.Name
                });                                               
            }
        }

        public void MapSkillsToSkillProf(List<Skill> skills)
        {          

            foreach (var item in skills)
            {
                Skills.Add(item.Id);
            }
        }

        public void MapSkillProficiencies()
        {
            var classProf = Class?.Skills ?? new List<Skill>();
            SkillProficiencies = classProf.ToList();
        }        
    }

    public class SheetSkill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbilityScore { get; set; }
    }
    
    public class ScoreIncrease
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public int IncreaseAmount { get; set; }
    }
    
}
