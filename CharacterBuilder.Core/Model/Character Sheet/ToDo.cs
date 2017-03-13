namespace CharacterBuilder.Core.Model
{
    public class ToDo
    {
        public int Id { get; set; }
        public bool HasSelectedClass { get; set; }
        public bool HasSelectedRace { get; set; }
        public bool HasSelectedSubRace { get; set; }
        public bool HasSelectedBackground { get; set; }        
        public bool HasSelectedSkills { get; set; }
        public bool HasCompletedAbilityScores { get; set; }
        public FirstLevelTasks FirstLevelTasks { get; set; } = new FirstLevelTasks();
    }

    public class FirstLevelTasks
    {
        public bool HasIncreasedHp { get; set; }
        public bool HasRolledStrength { get; set; }
        public bool HasRolledDexterity { get; set; }
        public bool HasRolledConstitution { get; set; }
        public bool HasRolledIntelligence { get; set; }
        public bool HasRolledWisdom { get; set; }
        public bool HasRolledCharisma { get; set; }
    }
}
