using System;
using System.Collections.Generic;
using CharacterBuilder.Core.Model.User;

namespace CharacterBuilder.Core.Model
{
    public class CharacterSheet
    {
        public CharacterSheet()
        {
            CreatedDate = DateTime.Now;            
        }

        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ToDo ToDo { get; set; }
        public string CharacterName { get; set; }
        public string PlayerName { get; set; }
        public Class Class { get; set; }
        public Background Background { get; set; }
        public int ClassLevel { get; set; }
        public int HitPointsMax { get; set; }
        public int Strength { get; set; }
        public int StrengthMod { get; set; }
        public int Dexterity { get; set; }
        public int DexterityMod { get; set; }
        public int Constitution { get; set; }
        public int ConstitutionMod { get; set; }
        public int Wisdom { get; set; }
        public int WisdomMod { get; set; }
        public int Intelligence { get; set; }
        public int IntelligenceMod { get; set; }
        public int Charisma { get; set; }
        public int CharismaMod { get; set; }
        public IList<Skill> Skills { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
