using System.Collections.Generic;
using CharacterBuilder.Core.Model.User;

namespace CharacterBuilder.Core.Model.DungeonMaster
{
    public class Campaign
    {
        public int Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Name { get; set; }
        public IList<PlayerCharacterCard> Players { get; set; }
    }
}
