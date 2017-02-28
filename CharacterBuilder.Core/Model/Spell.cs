using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterBuilder.Core.Model
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Class> Classes { get; set; }
    }
}
