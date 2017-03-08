using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CharacterBuilder.Core.Model.User;

namespace CharacterBuilder.Core.Model
{
    public class CharacterSheet : BaseEntity
    {
        public CharacterSheet()
        {
            CreatedDate = DateTime.Now;
            ClassLevel = 1;
        }
        
        public virtual ApplicationUser User { get; set; }
        public IList<AbilityScoreSheetValue> AbilityScores { get; set; }
        public AbilityScores AbilityScoreSection = new AbilityScores();
        public List<AbilityScoreIncrease> AbilityScoreIncreases { get; set; }
        public ToDo ToDo { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public Class Class { get; set; }
        public Background Background { get; set; }
        public Race Race { get; set; }
        public int ClassLevel { get; set; }
        public int HitPointsMax { get; set; }
        public IList<Skill> Skills { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }

        public TResult CreateScore<TResult>(Func<AbilityScores, TResult> action)
        {
            return action(AbilityScoreSection);
        }

        public void UpdateToDo(Action<ToDo> toDoToSetAction )
        {
            toDoToSetAction(ToDo);
        }

        //public override Update()
        //{
            
        //}

    }

    [ComplexType]
    public class AbilityScores
    {
        public int StrengthRoll { get; set; }
        public int StrengthMod { get; set; }

        public int DexterityRoll { get; set; }
        public int DexterityMod { get; set; }

        public int ConstitutionRoll { get; set; }
        public int ConstitutionMod { get; set; }

        public int WisdomRoll { get; set; }
        public int WisdomMod { get; set; }

        public int IntelligenceRoll { get; set; }
        public int IntelligenceMod { get; set; }

        public int CharismaRoll { get; set; }
        public int CharismaMod { get; set; }


    }
}
