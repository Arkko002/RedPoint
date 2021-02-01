using Microsoft.EntityFrameworkCore;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Data
{
    public interface IChatEntityRepositoryProxy<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity 
        where TContext: DbContext
    {
    }
}