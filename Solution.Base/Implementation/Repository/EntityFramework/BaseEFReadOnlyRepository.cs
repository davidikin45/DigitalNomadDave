﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Solution.Base.Interfaces.Model;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Interfaces.Persistance;

using System.Threading.Tasks;
using System.Data.Entity;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace Solution.Base.Implementation.Repository.EntityFramework
{
    public class BaseEFReadOnlyRepository<TContext, TEntity> : IBaseReadOnlyRepository<TEntity>
    where TContext : IBaseDbContext
    where TEntity : class, IBaseEntity
    {
        protected readonly TContext _context;
        protected readonly Boolean _tracking;
        protected readonly CancellationToken _cancellationToken;

        //AsNoTracking() causes EF to bypass cache for reads and writes - Ideal for Web Applications and short lived contexts

        public BaseEFReadOnlyRepository(TContext context, Boolean tracking, CancellationToken cancellationToken = default(CancellationToken))
        {
            this._context = context;
            this._tracking = tracking;
            this._cancellationToken = cancellationToken;
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            string search = "",
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            //includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (!_tracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = CreateSearchQuery(query, search);
            }

            if (includeProperties != null)
            {
                foreach (var includeExpression in includeProperties)
                {
                    query = query.Include(includeExpression);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            DebugSQL(query);

            return query;
        }

        private void DebugSQL(IQueryable<TEntity> query)
        {
            var sql = query.ToString();
        }

        protected virtual IQueryable<TEntity> CreateSearchQuery(IQueryable<TEntity> query, string value)
        {
            List<Expression> expressions = new List<Expression>();

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");

            MethodInfo contains_method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            foreach (PropertyInfo prop in typeof(TEntity).GetProperties().Where(x => x.PropertyType == typeof(string)))
            {
                MemberExpression member_expression = Expression.PropertyOrField(parameter, prop.Name);

                ConstantExpression value_expression = Expression.Constant(value, typeof(string));

                MethodCallExpression contains_expression = Expression.Call(member_expression, contains_method, value_expression);

                expressions.Add(contains_expression);
            }

            if (expressions.Count == 0)
                return query;

            Expression or_expression = expressions[0];

            for (int i = 1; i < expressions.Count; i++)
            {
                or_expression = Expression.OrElse(or_expression, expressions[i]);
            }

            Expression<Func<TEntity, bool>> expression = Expression.Lambda<Func<TEntity, bool>>(
                or_expression, parameter);

            return query.Where(expression);
        }

        public virtual IEnumerable<TEntity> GetAll(
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          int? skip = null,
          int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return GetQueryable(null, null, orderBy, skip, take, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            // string includeProperties = null,
            int? skip = null,
            int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return await GetQueryable(null, null, orderBy, skip, take, includeProperties).ToListAsync(_cancellationToken);
        }

        public virtual IEnumerable<TEntity> SQLQuery(string query, params object[] paramaters)
        {
            return _context.SQLQueryNoTracking<TEntity>(query, paramaters);
        }

        public async virtual Task<IEnumerable<TEntity>> SQLQueryAsync(string query, params object[] paramaters)
        {
            return await _context.SQLQueryNoTrackingAsync<TEntity>(query, paramaters);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //  string includeProperties = null,
            int? skip = null,
            int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return GetQueryable(null, filter, orderBy, skip, take, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //string includeProperties = null,
            int? skip = null,
            int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return await GetQueryable(null, filter, orderBy, skip, take, includeProperties).ToListAsync(_cancellationToken);
        }

        public virtual IEnumerable<TEntity> Search(
            string search = "",
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //  string includeProperties = null,
            int? skip = null,
            int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return GetQueryable(search, filter, orderBy, skip, take, includeProperties).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> SearchAsync(
             string search = "",
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            //string includeProperties = null,
            int? skip = null,
            int? take = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return await GetQueryable(search, filter, orderBy, skip, take, includeProperties).ToListAsync(_cancellationToken);
        }

        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
          // string includeProperties = "",
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return GetQueryable(null, filter, null, null, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
          // string includeProperties = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return await GetQueryable(null, filter, null, null, null, includeProperties).SingleOrDefaultAsync(_cancellationToken);
        }

        public virtual TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          //string includeProperties = "",
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return GetQueryable(null, filter, orderBy, null, null, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          // string includeProperties = null,
          params Expression<Func<TEntity, Object>>[] includeProperties)
        {
            return await GetQueryable(null, filter, orderBy, null, null, includeProperties).FirstOrDefaultAsync(_cancellationToken);
        }

        public virtual TEntity GetById(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public async virtual Task<TEntity> GetByIdAsync(object id)
        {
            return await GetQueryable(null, x => x.Id.ToString() == id.ToString(), null, null).SingleOrDefaultAsync(_cancellationToken);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(null, filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(null, filter).CountAsync(_cancellationToken);
        }

        public virtual int GetSearchCount(string search = "", Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(search, filter).Count();
        }

        public virtual Task<int> GetSearchCountAsync(string search = "", Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(search, filter).CountAsync(_cancellationToken);
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(null, filter).Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(null, filter).AnyAsync(_cancellationToken);
        }
    }
}
