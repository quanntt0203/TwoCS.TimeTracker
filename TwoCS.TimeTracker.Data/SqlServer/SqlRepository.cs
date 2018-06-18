
namespace TwoCS.TimeTracker.Data.SqlServer
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using TwoCS.TimeTracker.Core.Repositories;
    using TwoCS.TimeTracker.Domain.Models;

    public class SqlRepository<T> : RepositoryBase<T> where T : ModelBase
    {
        public override T Create(T model)
        {
            throw new NotImplementedException();
        }

        public override Task<T> CreateAsync(T model)
        {
            throw new NotImplementedException();
        }

        public override void Delete(T model)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(T model)
        {
            throw new NotImplementedException();
        }

        public override T Read(string key)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<T> ReadAll(Expression<Func<T, bool>> pression = null)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<T>> ReadAllAsync(Expression<Func<T, bool>> pression = null)
        {
            throw new NotImplementedException();
        }

        public override Task<T> ReadAsync(string key)
        {
            throw new NotImplementedException();
        }

        public override T Single(Expression<Func<T, bool>> pression)
        {
            throw new NotImplementedException();
        }

        public override Task<T> SingleAsync(Expression<Func<T, bool>> pression)
        {
            throw new NotImplementedException();
        }

        public override T Update(T model)
        {
            throw new NotImplementedException();
        }

        public override Task<T> UpdateAsync(T model)
        {
            throw new NotImplementedException();
        }
    }
}
