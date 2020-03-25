using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedPoint.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity Find(params object[] keys);
        IQueryable<TEntity> Query { get; }
    }
}