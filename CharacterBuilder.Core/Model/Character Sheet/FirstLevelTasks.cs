namespace CharacterBuilder.Core.Model
{
    public class FirstLevelTasks : BaseEntity
    {
        public bool HasRolledHp { get; set; }
        public bool HasRolledStrength { get; set; }
        public bool HasRolledDexterity { get; set; }
        public bool HasRolledConstitution { get; set; }
        public bool HasRolledIntelligence { get; set; }
        public bool HasRolledWisdom { get; set; }
        public bool HasRolledCharisma { get; set; }
    }
}
