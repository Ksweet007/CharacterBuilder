using System.Collections.Generic;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class AbilityScoreIncrease : BaseEntity
    {
        public AbilityScore AbilityScore { get; set; }
        public int IncreaseValue { get; set; }
        public IList<CharacterSheet> CharacterSheets { get; set; }
        public IList<Race> Races { get; set; }
        public IList<Subrace> SubRaces { get; set; } 
    }
}
