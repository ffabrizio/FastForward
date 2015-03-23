using System;
using MongoRepository;

namespace FastForward.Data.Mongo
{
    public interface IRepositoryLocator
    {
        MongoRepository<DataEntity> this[string collectionKey] { get; }
        MongoRepository<DataEntity> this[Type type] { get; }
    }
}