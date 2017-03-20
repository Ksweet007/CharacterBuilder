using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharacterBuilder.Core.Model.DungeonMaster;

namespace CharacterBuilder.Core.DTO
{
    public class PlayerCharacterCardDto
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string PlayerName { get; set; }
        public string CharacterName { get; set; }
        public int PassivePerception { get; set; }
        public string HitPoints { get; set; }
        public int ArmorClass { get; set; }
        public PlayerCardSaves Saves { get; set; }
    }
}
