using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class ToolType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<ToolOption> ToolOptions { get; set; }
    }
}
