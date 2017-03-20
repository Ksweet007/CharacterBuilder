using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterBuilder.Core.DTO
{
    public class CharacterSheetMinimalDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CharacterName { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Background { get; set; }
        public string Class { get; set; }
    }
}
