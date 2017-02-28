using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class Race
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AgeDescription { get; set; }
        public Size Size { get; set; }
        public string Speed { get; set; }
        public Alignment Alignment { get; set; }
        public IList<Language> Languages { get; set; }
        public IList<AbilityScoreIncrease> AbilityScoreIncreases { get; set; }
        public IList<RaceFeature> RacialFeatures { get; set; }
        public IList<Subrace> Subraces { get; set; }
        public IList<Proficiency> Proficiencies { get; set; }
    }
}
