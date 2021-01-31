using Microsoft.EntityFrameworkCore;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{
    public interface IChatEntityRepositoryProxy<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity 
        where TContext: DbContext
    {
    }
}