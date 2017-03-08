using System.Linq;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public static class BaseRepository
    {
        private static readonly CharacterBuilderDbContext _dbContext;

        static BaseRepository()
        {
            _dbContext = new CharacterBuilderDbContext();
        }

        public static T GetById<T>(int id) where T : BaseEntity
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }
    }
}
