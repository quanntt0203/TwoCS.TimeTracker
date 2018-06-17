namespace TwoCS.TimeTracker.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Domain.Models;

    public abstract class RepositoryBase<T> : IRepository<T> where T : ModelBase
    {

        protected string UUID => Guid.NewGuid().ToString();
    
        public abstract T Create(T model);

        public abstract Task<T> CreateAsync(T model);

        public abstract void Delete(T model);


        public abstract Task DeleteAsync(T model);

        public abstract T Read(string key);

        public abstract IEnumerable<T> ReadAll(Expression<Func<T, bool>> pression = null);

        public abstract Task<IEnumerable<T>> ReadAllAsync(Expression<Func<T, bool>> pression = null);

        public abstract Task<T> ReadAsync(string key);

        public abstract T Single(Expression<Func<T, bool>> pression);

        public abstract Task<T> SingleAsync(Expression<Func<T, bool>> pression);

        public abstract T Update(T model);

        public abstract Task<T> UpdateAsync(T model);
    }
}
