namespace TwoCS.TimeTracker.Data.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using TwoCS.TimeTracker.Domain.Models;
    using TwoCS.TimeTracker.Core.Repositories;

    public class MongoRepository<T> : RepositoryBase<T> where T : ModelBase
    {
        protected Lazy<IMongoDatabase> Database => DataFactory.MongoDatabase;

        protected string CollectionName => DataConfiguration.GetCollectionName<T>();
        
        protected IMongoCollection<T> Collection => Database.Value.GetCollection<T>(CollectionName);

        public MongoRepository()
        {
        }

        public override T Create(T model)
        {
            model.Id = UUID;
            Collection.InsertOne(model);
            return Read(model.Id);
        }

        public async override Task<T> CreateAsync(T model)
        {
            model.Id = UUID;
            await Collection.InsertOneAsync(model);
            return await ReadAsync(model.Id);
        }

        public override void Delete(T model)
        {
            Collection.DeleteOne(s => s.Id == model.Id);
        }

        public async override Task DeleteAsync(T model)
        {
            await Collection.DeleteOneAsync(s => s.Id == model.Id);
        }

        public override T Read(string key)
        {
            return Collection.Find(s => s.Id == key)
                .SingleOrDefault();
        }

        public override IEnumerable<T> ReadAll(Expression<Func<T, bool>> pression = null)
        {
            var filter = pression ?? Builders<T>.Filter.Empty;
            return Collection.Find(filter)
                .ToList();
        }

        public async override Task<IEnumerable<T>> ReadAllAsync(Expression<Func<T, bool>> pression = null)
        {
            var filter = pression ?? Builders<T>.Filter.Empty;

            var result = await Collection.FindAsync(filter);

            return await result.ToListAsync();
        }

        public async override Task<T> ReadAsync(string key)
        {
            return await (await Collection.FindAsync(s => s.Id == key))
               .SingleOrDefaultAsync();
        }

        public override T Single(Expression<Func<T, bool>> pression)
        {
            var filter = pression ?? Builders<T>.Filter.Empty;
            return Collection.Find(filter)
                .SingleOrDefault();
        }

        public async override Task<T> SingleAsync(Expression<Func<T, bool>> pression)
        {
            var filter = pression ?? Builders<T>.Filter.Empty;
            return await (await Collection.FindAsync(filter))
                .SingleOrDefaultAsync();
        }

        public override T Update(T model)
        {
            Collection.FindOneAndReplace(s => s.Id == model.Id, model);
            return Read(model.Id);
        }

        public async override Task<T> UpdateAsync(T model)
        {
            await Collection.FindOneAndReplaceAsync(s => s.Id == model.Id,  model);
            return await ReadAsync(model.Id);
        }
    }
}
