using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class ClassLevelEvent : BaseEntity
    {
        public bool HasIncreasedHp { get; set; }
        public bool HasIncreasedAbilityScore { get; set; }
        public bool HasAbilityScoreIncrease { get; set; }
        public List<int> AbilityScoreIncreaseLevels { get; set; }
    }
}
