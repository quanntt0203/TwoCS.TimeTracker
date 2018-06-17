namespace TwoCS.TimeTracker.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Domain.Models;

    public class ServiceBase<T> : IService<T> where T : ModelBase
    {
        protected readonly IRepository<T> Repository;

        public ServiceBase(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual T Create(T model)
        {
            return Repository.Create(model);
        }

        public virtual async Task<T> CreateAsync(T model)
        {
            return await Repository.CreateAsync(model);
        }

        public virtual void Delete(T model)
        {
            Repository.Delete(model);
        }

        public virtual async Task DeleteAsync(T model)
        {
            await Repository.DeleteAsync(model);
        }

        public virtual T Read(string key)
        {
            return Repository.Read(key);
        }

        public IEnumerable<T> ReadAll()
        {
            return Repository.ReadAll();
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Repository.ReadAllAsync();
        }

        public virtual async Task<T> ReadAsync(string key)
        {
            return await  Repository.ReadAsync(key);
        }

        public virtual T Update(T model)
        {
            return Repository.Update(model);
        }

        public virtual async Task<T> UpdateAsync(T model)
        {
            return await Repository.UpdateAsync(model);
        }
    }
}
