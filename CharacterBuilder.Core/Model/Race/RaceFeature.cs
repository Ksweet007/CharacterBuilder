using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterBuilder.Core.Enums;

namespace CharacterBuilder.Core.Model
{
    public class RaceFeature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BonusValue { get; set; }
        public FeatureBonusType BonusType { get; set; }
    }
}
