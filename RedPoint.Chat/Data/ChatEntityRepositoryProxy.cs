using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedPoint.Chat.Exceptions;
using RedPoint.Data.Repository;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Chat.Data
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
        private EntityUnitOfWork _unitOfWork;
        
        public ChatEntityRepositoryProxy(EntityRepository<TEntity, TContext> entityRepository, EntityUnitOfWork unitOfWork)
        {
            _entityRepository = entityRepository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> Query { get; }

        public void Add(TEntity entity)
        {
            _entityRepository.Add(entity);
            _unitOfWork.Submit();
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