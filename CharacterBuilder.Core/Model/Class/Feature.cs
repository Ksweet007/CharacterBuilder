using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Levelgained { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
