namespace TwoCS.TimeTracker.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Domain.Models;

    public interface IService<T> where T : ModelBase
    {
        T Create(T model);

        Task<T> CreateAsync(T model);


        T Update(T model);

        Task<T> UpdateAsync(T model);


        void Delete(T model);

        Task DeleteAsync(T model);


        T Read(string key);

        Task<T> ReadAsync(string key);

        IEnumerable<T> ReadAll();

        Task<IEnumerable<T>> ReadAllAsync();

        IEnumerable<T> ReadAll(Expression<Func<T, bool>> pression = null);

        Task<IEnumerable<T>> ReadAllAsync(Expression<Func<T, bool>> pression = null);
    }
}
