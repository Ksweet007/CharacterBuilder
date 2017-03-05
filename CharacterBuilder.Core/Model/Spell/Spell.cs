using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Class> Classes { get; set; }
    }
}
