using System.Collections.Generic;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Core.Interfaces
{
    public interface IBaseEfRepository
    {
        T GetById<T>(int id) where T : BaseEntity;
        List<T> List<T>() where T : BaseEntity;
        T Add<T>(T entity) where T : BaseEntity;
        void Delete<T>(T entity) where T : BaseEntity;
        T Update<T>(T entity) where T : BaseEntity;
    }
}
