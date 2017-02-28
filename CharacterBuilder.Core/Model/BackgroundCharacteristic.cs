using System.Collections.Generic;

namespace CharacterBuilder.Core.Model
{
    public class BackgroundCharacteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<BackgroundOption> BackgroundOptions { get; set; }
    }
}
