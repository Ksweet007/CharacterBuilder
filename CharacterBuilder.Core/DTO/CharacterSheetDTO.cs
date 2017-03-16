using System;
using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class CharacterSheetDTO : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int Level { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Alignment { get; set; }
        public int HpMax { get; set; }


        public IList<SkillListingDTO> SkillsSelected { get; set; }  

        public IList<ProficiencyBonus> ProficiencyBonuses { get; set;}

        public Class Class { get; set; } = new Class();        
        public Background Background { get; set; }
        public Race Race { get; set; }
        public Subrace Subrace { get; set; }
        

        public AbilityScores AbilityScores { get; set; }
        public IList<ScoreIncrease> AbilityScoreIncreases { get; set; } = new List<ScoreIncrease>();


        public ToDo ToDo { get; set; } = new ToDo();
        public LevelChecklist LevelChecklist { get; set; } //Only load one per level, once saved, grab new one from DB
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
      
    }
   
    public class ScoreIncrease
    {
        public int Id { get;set; }
        public string Name { get; set; }
        public int IncreaseAmount { get; set; }
    }
    
}
