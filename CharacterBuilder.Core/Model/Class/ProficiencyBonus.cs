using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class ProficiencyBonus
    {
        public int Id { get; set; }
        public int ClassId { get; set;}
        public int Level { get; set; }
        public int BonusValue { get; set; }
    }
}
