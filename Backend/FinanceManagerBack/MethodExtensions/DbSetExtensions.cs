using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FinanceManagerBack.MethodExtensions
{
    public static class DbSetExtensions
    {
        public static EntityEntry<T> AddIfNotExists<T>(this DbSet<T> dbSet, T entity, Expression<Func<T, bool>> predicate = null) where T : class, new()
        {
            var exists = dbSet.Where(predicate).FirstOrDefault() != null;
            return exists ? null : dbSet.Add(entity);
        }
    }
}