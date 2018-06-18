namespace TwoCS.TimeTracker.Data
{
    using System;
    using Microsoft.Extensions.Configuration;
    using MongoDB.Driver;
    using TwoCS.TimeTracker.Core.Factories;
    using TwoCS.TimeTracker.Domain.Models;

    public class DataFactory
    {
        private static readonly IConfiguration _configuration;

        static DataFactory()
        {
            _configuration = ResolverFactory.GetService<IConfiguration>();
        }

        public static Lazy<IMongoDatabase> MongoDatabase
        {
            get
            {
                var config = _configuration.GetSection("Storage:Mongo:TimeTracker");
                return new Lazy<IMongoDatabase>(() =>
                {
                    return new MongoClient(config.GetValue<string>("ConnectionString")).GetDatabase(config.GetValue<string>("DbName"));
                });
            }
        }

        public static IMongoCollection<T> GetMongoCollection<T>() where T : ModelBase
        {
            var collectionName = DataConfiguration.GetCollectionName<T>();
            return MongoDatabase.Value.GetCollection<T>(collectionName);
        }
    }
}
