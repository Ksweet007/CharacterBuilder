using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class ClassRepository
    {
        private readonly CharacterBuilderDbContext _db;

        public ClassRepository()
        {
            _db = new CharacterBuilderDbContext();
        }

        public IList<Class> GetAllClasses()
        {
            return _db.Classes.Include(s => s.Skills).ToList();
        }

        
    }
}
