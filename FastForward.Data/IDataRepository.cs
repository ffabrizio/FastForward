using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FastForward.Data
{
    public interface IDataRepository
    {
        T GetById<T>(string id) where T : DataEntity;
        void Delete<T>(string id) where T : DataEntity;
        void DeleteAll<T>() where T : DataEntity;
        IList<T> Find<T>(Expression<Func<T, bool>> predicate) where T : DataEntity;
        T FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : DataEntity;
        T FirstOrNew<T>(Expression<Func<T, bool>> predicate) where T : DataEntity, new();
        T Create<T>(T entity) where T : DataEntity;
        bool Update(DataEntity entity);
        bool CreateOrUpdate(DataEntity entity);
    }
}