namespace CharacterBuilder.Core.DTO
{
    public class SkillListingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AbilityScoreName { get; set; }
        public bool IsLockedChoice { get; set; }
        public bool IsSelected { get; set; }
    }
}
