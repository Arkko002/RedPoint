using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RedPoint.Data.UnitOfWork
{
    public class EntityUnitOfWork : Disposable, IUnitOfWork
    {
        private DbContext _context;

        public bool HasEnded { get; private set; }

        public EntityUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public TDbContext GetContext<TDbContext>() where TDbContext : DbContext
        {
            if (HasEnded)
            {
                throw new ObjectDisposedException("Unit of Work has been disposed.");
            }

            return (TDbContext)_context;
        }

        public void Submit()
        {
            if (HasEnded)
            {
                throw new ObjectDisposedException("Unit of Work has been disposed.");
            }

            if (_context != null)
            {
                RulesService.ApplyInsertRules(_context.Changes(EntityState.Added));
                RulesService.ApplyDeleteRules(_context.Changes(EntityState.Modified));
                RulesService.ApplyUpdateRules(_context.Changes(EntityState.Deleted));

                _context.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!HasEnded)
            {
                _context?.Dispose();
                HasEnded = true;
            }
            base.Dispose(disposing);
        }
    }

    public static class DbContextExtensions
    {
        public static IEnumerable<object> Changes(this DbContext context, EntityState state)
        {
            return context.ChangeTracker.Entries().Where(x => x.State == state).Select(x => x.Entity);
        }
    }
}