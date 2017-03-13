using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.DTO
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public bool HasSelectedClass { get; set; }
        public bool HasSelectedRace { get; set; }
        public bool HasSelectedSubRace { get; set; }
        public bool HasSelectedBackground { get; set; }
        public bool HasCompletedAbilityScores { get; set; }
        public bool HasSelectedSkills { get; set; }
        public FirstLevelTasks FirstLevelTasks { get; set; }
        public bool CharacterCreationComplete { get; set; }

    }
}
