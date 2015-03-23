using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FastForward.Data.Mongo;

namespace FastForward.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly IRepositoryLocator locator;

        public DataRepository(IRepositoryLocator mongoLocator)
        {
            locator = mongoLocator;
        }

        public T GetById<T>(string id) where T : DataEntity
        {
            return locator[typeof(T)].GetById(id) as T;
        }

        public void Delete<T>(string id) where T : DataEntity
        {
            locator[typeof(T)].Delete(id);
        }

        public void DeleteAll<T>() where T : DataEntity
        {
            locator[typeof(T)].DeleteAll();
        }

        public IList<T> Find<T>(Expression<Func<T, bool>> predicate) where T : DataEntity
        {
            return locator[typeof(T)]
                .OfType<T>()
                .AsQueryable()
                .Where(predicate.Compile())
                .OrderByDescending(e => e.Created)
                .ToList();
        }

        public T FirstOrDefault<T>(Expression<Func<T, bool>> expression) where T : DataEntity
        {
            return locator[typeof(T)]
                .OfType<T>()
                .OrderByDescending(e => e.Created)
                .FirstOrDefault(expression);
        }

        public T FirstOrNew<T>(Expression<Func<T, bool>> expression) where T : DataEntity, new()
        {
            return FirstOrDefault(expression) ?? new T();
        }

        public T Create<T>(T entity) where T : DataEntity
        {
            if (!string.IsNullOrEmpty(entity.Id))
            {
                return null;
            }

            try
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Created = DateTime.UtcNow;

                return locator[entity.CollectionName].Add(entity) as T;
            }
            catch (Exception exception)
            {
                //this.LogError("Unable to add entity to repository", exception);

                return null;
            }
        }

        public bool Update(DataEntity entity)
        {
            var entityUpdated = entity.Updated;

            try
            {
                entity.Updated = DateTime.UtcNow;
                locator[entity.CollectionName].Update(entity);

                return true;
            }
            catch (Exception exception)
            {
                //this.LogError("Unable to update entity", exception);

                entity.Updated = entityUpdated;

                return false;
            }
        }

        public bool CreateOrUpdate(DataEntity entity)
        {
            if (!string.IsNullOrEmpty(entity.Id))
            {
                return Update(entity);
            }

            return (Create(entity) != null);
        }
    }
}