using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class ToolOption
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IList<Tool> Tools { get; set; }
        public ToolType ToolType { get; set; }
    }
}
