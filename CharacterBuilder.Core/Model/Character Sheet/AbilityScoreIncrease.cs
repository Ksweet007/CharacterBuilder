using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class AbilityScoreIncrease
    {
        public int Id { get; set; }
        public AbilityScore AbilityScore { get; set; }
        public int IncreaseValue { get; set; }
    }
}
