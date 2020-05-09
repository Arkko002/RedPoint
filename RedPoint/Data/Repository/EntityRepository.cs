using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Data.Repository
{
    public class EntityRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        public EntityRepository(EntityUnitOfWork unitOfWork)
        {
            Context = unitOfWork.GetContext<TContext>();
        }

        protected TContext Context { get; }
        protected DbSet<TEntity> Set => Context.Set<TEntity>();

        public IQueryable<TEntity> Query => Set;

        public void Add(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            Set.Remove(entity);
        }

        public TEntity Find(params object[] keys)
        {
            return Set.Find(keys);
        }

        public void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }
    }
}