using System;
using System.Collections.Generic;

namespace RedPoint.Data
{
    public static class RulesService
    {
        private static readonly List<Action<object>> DeleteRules = new List<Action<object>>();
        private static readonly List<Action<object>> InsertRules = new List<Action<object>>();
        private static readonly List<Action<object>> UpdateRules = new List<Action<object>>();

        public static void AddDeleteRule(Action<object> deleteRule)
        {
            DeleteRules.Add(deleteRule);
        }

        public static void AddDeleteRule<TEntity>(Action<TEntity> deleteRule)
        {
            DeleteRules.Add(x =>
            {
                if (x is TEntity entity) deleteRule(entity);
            });
        }

        public static void ApplyDeleteRules(IEnumerable<object> deleting)
        {
            foreach (var entity in deleting)
            foreach (var rule in DeleteRules)
                rule(entity);
        }

        public static void AddInsertRule(Action<object> insertRule)
        {
            InsertRules.Add(insertRule);
        }

        public static void AddInsertRule<TEntity>(Action<TEntity> insertRule)
        {
            InsertRules.Add(x =>
            {
                if (x is TEntity entity) insertRule(entity);
            });
        }

        public static void ApplyInsertRules(IEnumerable<object> inserting)
        {
            foreach (var entity in inserting)
            foreach (var rule in InsertRules)
                rule(entity);
        }

        public static void AddUpdateRule(Action<object> updateRule)
        {
            UpdateRules.Add(updateRule);
        }

        public static void AddUpdateRule<TEntity>(Action<TEntity> updateRule)
        {
            UpdateRules.Add(x =>
            {
                if (x is TEntity entity) updateRule(entity);
            });
        }

        public static void ApplyUpdateRules(IEnumerable<object> updating)
        {
            foreach (var entity in updating)
            foreach (var rule in UpdateRules)
                rule(entity);
        }
    }
}