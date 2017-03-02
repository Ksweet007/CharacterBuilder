using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class BackgroundOption
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public BackgroundCharacteristic BackgroundCharacteristic { get; set; }
    }
}
