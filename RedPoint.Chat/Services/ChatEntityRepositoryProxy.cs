using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedPoint.Chat.Data;
using RedPoint.Chat.Exceptions;
using RedPoint.Chat.Models.Chat;
using RedPoint.Data.Repository;

namespace RedPoint.Chat.Services
{
    //TODO Improper implementation of proxy pattern
    /// <summary>
    /// Proxy for <c>EntityRepository</c> objects that provides error handling for repository operations.
    /// </summary>
    public class ChatEntityRepositoryProxy<TEntity, TContext> : IChatEntityRepositoryProxy<TEntity, TContext>
        where TEntity : class, IEntity
        where TContext : DbContext
    {

        private EntityRepository<TEntity, TContext> _entityRepository;

        public ChatEntityRepositoryProxy(EntityRepository<TEntity, TContext> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public IQueryable<TEntity> Query { get; }

        public void Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Find(params object[] keys)
        {
            var entity = _entityRepository.Find(keys);
            
            if(entity == null)
            {
                throw new EntityNotFoundException("Required entity could not be found.", keys);
            }

            return entity;
        }
    }
}