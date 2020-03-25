using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Data
{
    public class EntityRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        protected TContext Context { get; private set; }
        protected DbSet<TEntity> Set => Context.Set<TEntity>();

        public EntityRepository(EntityUnitOfWork unitOfWork)
        {
            Context = unitOfWork.GetContext<TContext>();
        }

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