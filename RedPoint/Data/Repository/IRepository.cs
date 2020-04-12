using System.Linq;

namespace RedPoint.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> Query { get; }

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        TEntity Find(params object[] keys);
    }
}