using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CharacterBuilder.Core.Model.User;

namespace CharacterBuilder.Core.Model
{
    public class CharacterSheet
    {
        public CharacterSheet()
        {
            CreatedDate = DateTime.Now;
            ClassLevel = 1;
        }
        
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public AbilityScores AbilityScores { get; set; }
        public IList<AbilityScoreIncrease> AbilityScoreIncreases { get; set; } 
        public ToDo ToDo { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public string Alignment { get; set; }
        public Class Class { get; set; }
        public Background Background { get; set; }
        public Race Race { get; set; }
        public int ClassLevel { get; set; }
        public int HitPointsMax { get; set; }
        public IList<Skill> Skills { get; set; }
        public bool IsComplete { get; set; }
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
