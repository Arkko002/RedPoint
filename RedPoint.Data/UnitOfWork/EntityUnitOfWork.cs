using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RedPoint.Data.UnitOfWork
{
    public class EntityUnitOfWork : Disposable, IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly RulesService _rules;

        public EntityUnitOfWork(DbContext context, RulesService rules)
        {
            _context = context;
            _rules = rules;
        }

        public bool HasEnded { get; private set; }

        public void Submit()
        {
            if (HasEnded)
            {
                throw new ObjectDisposedException("Unit of Work has been disposed.");
            }

            if (_context != null)
            {
                _rules.ApplyInsertRules(_context.Changes(EntityState.Added));
                _rules.ApplyDeleteRules(_context.Changes(EntityState.Modified));
                _rules.ApplyUpdateRules(_context.Changes(EntityState.Deleted));

                _context.SaveChanges();
            }
        }

        public TDbContext GetContext<TDbContext>() where TDbContext : DbContext
        {
            if (HasEnded)
            {
                throw new ObjectDisposedException("Unit of Work has been disposed.");
            }

            return (TDbContext)_context;
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