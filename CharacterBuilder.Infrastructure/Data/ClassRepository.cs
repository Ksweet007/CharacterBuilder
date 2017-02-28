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

        
    }
}
