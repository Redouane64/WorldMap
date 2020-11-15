using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace WorldMap.Common.Data.Repositories
{
    public abstract class Repository<TKey, T> : IRepository<TKey, T> where T : class, new()
    {
        protected IDbConnection Connection { get; }

        protected Repository(IDbConnection connection) => Connection = connection;

        protected abstract void Map(IDataReader reader, T entity);

        protected IEnumerable<T> GetAll(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var countries = new List<T>();
                while (reader.Read())
                {
                    var country = new T();
                    Map(reader, country);
                    countries.Add(country);
                }
                return countries;
            }
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T Get(TKey id);

        [Obsolete("This method is not supported.")]
        public T Get(int id) => throw new NotSupportedException();

        [Obsolete("This method is not supported.")]
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => throw new NotSupportedException();

        public abstract void Add(T entity);

        public abstract void AddRange(IEnumerable<T> entities);

        public abstract void Update(T entity);

        public abstract void Delete(T entity);

    }
}
