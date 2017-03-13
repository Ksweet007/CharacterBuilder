using System;
using System.Collections.Generic;
using CharacterBuilder.Core.Model.User;

namespace CharacterBuilder.Core.Model
{
    public class CharacterSheet : BaseEntity
    {
        public CharacterSheet()
        {
            CreatedDate = DateTime.Now;
            ClassLevel = 1;
            ToDo = new ToDo();
            CreatedDate = DateTime.UtcNow;
        }
        
        public virtual ApplicationUser User { get; set; }
        public AbilityScores AbilityScores { get; set; } = new AbilityScores();
        public IList<AbilityScoreIncrease> AbilityScoreIncreases { get; set; } = new List<AbilityScoreIncrease>();
        public ToDo ToDo { get; set; }
        public IList<LevelChecklist> LevelChecklists { get; set; } = new List<LevelChecklist>();
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Alignment { get; set; }
        public Class Class { get; set; }
        public Background Background { get; set; }
        public Race Race { get; set; }
        public Subrace Subrace { get; set; }
        public int ClassLevel { get; set; }
        public int HitPointsMax { get; set; }
        public List<Skill> Skills { get; set; }= new List<Skill>();
        public DateTime CreatedDate { get; set; }
    }

    public class AbilityScores
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
    }
    
}
