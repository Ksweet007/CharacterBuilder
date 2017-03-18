namespace CharacterBuilder.Core.Model.DungeonMaster
{
    public class PlayerCharacterCard
    {
        public int Id { get; set; }
        public Campaign Campaign { get; set; }
        public string Name { get; set; }
        public int PassivePerception { get; set; }
        public string HitPoints { get; set; }
        public int ArmorClass { get; set; }
        public PlayerCardSaves Saves { get; set; }
    }

    public class PlayerCardSaves
    {
        public int StrengthSave { get; set; }
        public int Dexterity { get; set; }
        public int ConstitutionSave { get; set; }
        public int IntelligenceSave { get; set; }
        public int WisdomSave { get; set; }
        public int CharismaSave { get; set; }
    }
}
