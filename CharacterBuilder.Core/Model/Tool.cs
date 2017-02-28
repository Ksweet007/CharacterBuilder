using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Tool
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public string Weight { get; set; }
        public ProficiencyType ProficiencyTypeId { get; set; }
    }
}
