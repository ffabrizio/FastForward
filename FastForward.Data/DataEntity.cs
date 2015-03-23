using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace FastForward.Data
{
    public abstract class DataEntity : IEntity<string>
    {
        [BsonIgnore]
        public abstract string CollectionName { get; }
        public string Id { get; set; }
        public string Lang { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}