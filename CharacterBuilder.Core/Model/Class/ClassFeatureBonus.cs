using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterBuilder.Core.Model
{
    public class ClassFeatureBonus : BaseEntity
    {
        public string NameToIncrease { get; set; }
        public int IncreaseValue { get; set; }
    }
}
