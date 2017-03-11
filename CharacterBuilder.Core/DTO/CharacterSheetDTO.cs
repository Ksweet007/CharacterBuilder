using System;
using System.Collections.Generic;
using System.Linq;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class CharacterSheetDTO : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public Class Class { get; set; }
        public int Level { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Alignment { get; set; }
        public Background Background { get; set; }
        public Race Race { get; set; }
        public Subrace Subrace { get; set; }
        public IList<SheetSkill> Skills { get; set; }  = new List<SheetSkill>();
        public IList<Skill> AllSkills { get; set; }
        public IList<Skill> SkillProficiencies { get; set; }  = new List<Skill>();
        public ToDo ToDo { get; set; }
        public bool IsComplete { get; set; }
        public int HpMax { get; set; }        
        public AbilityScores AbilityScores { get; set; }
        public IList<ScoreIncrease> AbilityScoreIncreases { get; set; } = new List<ScoreIncrease>();

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

        public void MapSkillsToSkillProf(IList<Skill> skills)
        {          

            foreach (var item in skills)
            {
                Skills.Add(new SheetSkill
                {
                    Id = item.Id,
                    Name = item.Name,
                    AbilityScore = item.AbilityScore.Name
                });
            }
        }

        public void MapSkillProficiencies()
        {
            var classProf = Class.Skills;
            var backgroundProf = Background.Skills;
            SkillProficiencies = classProf.Concat(backgroundProf).Distinct().ToList();
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
