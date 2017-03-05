using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class Subrace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<RaceFeature> RacialFeature { get; set; }
    }
}
