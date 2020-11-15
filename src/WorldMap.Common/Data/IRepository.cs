using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WorldMap.Common.Data
{
	public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);
    }

    public interface IRepository<TKey, T> : IRepository<T> where T : class
    {
        T Get(TKey id);
    }
}