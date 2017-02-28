using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LanguageType LanguageType { get; set; }        
        public string ScriptName { get; set; }
        public string Description { get; set; }
    }
}
