using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class BackgroundRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public BackgroundRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Background> GetAllBackgrounds()
        {
            return
                _db.Backgrounds.Include(b => b.BackgroundCharacteristic.Select(y => y.BackgroundOptions))
                    .Include(s => s.Skills)
                    .ToList();
        }
    }
}
