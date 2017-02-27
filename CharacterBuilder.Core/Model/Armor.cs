namespace CharacterBuilder.Core.Model
{
    public class Armor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProficiencyId { get; set; }
        public Proficiency Proficiency { get; set; }
        public string Cost { get; set; }
        public string ArmorClass { get; set; }
        public string Strength { get; set; }
        public bool Stealth { get; set; }
        public string Weight { get; set; }
    }
}
