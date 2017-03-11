using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Infrastructure.Data.Contexts;

namespace CharacterBuilder.Infrastructure.Data
{
    public class BaseEfRepository
    {
        private static readonly CharacterBuilderDbContext _dbContext;

        static BaseEfRepository() 
        {
            _dbContext = new CharacterBuilderDbContext();
        }

        public T GetById<T>(int id) where T: BaseEntity
        {
            return _dbContext.Set<T>().SingleOrDefault(e => e.Id == id);
        }

        public List<T> List<T>() where T : BaseEntity
        {
            return _dbContext.Set<T>().ToList();
        }

        public IEnumerable<T> ListItems<T> (Expression<Func<T,bool>> predicate ) where T : BaseEntity
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public T Add<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public T Update<T>(T entity) where T : BaseEntity
        {
            _dbContext.SaveChanges();
            return entity;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
