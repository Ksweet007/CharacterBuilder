using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
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

        //public int Strengthh => CreateScore(s => s.Strength);

        public void GetScore(string scoreToGet)
        {
            var score = CreateScore(s => s.Strength);
            var totalBonuses = AbilityScoreIncreases.Where(x => x.AbilityScore.Name == "Strength").ToList().Sum(w => w.IncreaseValue);
        }

        public TResult CreateScore<TResult>(Func<AbilityScores, TResult> action)
        {
            return action(AbilityScoreSection);
        }

    }

    [ComplexType]
    public class AbilityScores
    {
        public int Strength;
        public int InitialStrength { get; set; }
        public int StrengthMod { get; set; }

        public int Dexterity;
        public int InitialDexterity { get; set; }
        public int DexterityMod { get; set; }

        public int Constitution;
        public int InitialConstitution { get; set; }
        public int ConstitutionMod { get; set; }

        public int Wisdom;
        public int InitialWisdom { get; set; }
        public int WisdomMod { get; set; }

        public int Intelligence;
        public int InitialIntelligence { get; set; }
        public int IntelligenceMod { get; set; }

        public int Charisma;
        public int InitialCharisma { get; set; }
        public int CharismaMod { get; set; }


    }
}
