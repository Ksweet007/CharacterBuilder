namespace CharacterBuilder.Core.Model
{
    public class LevelChecklist
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public bool HasAbilityScoreIncrease { get; set; }
        public bool HasIncreasedHp { get; set; }
        public bool HasIncreasedAbilityScores { get; set; }
        public CharacterSheet CharacterSheet { get; set; }
    }
}
