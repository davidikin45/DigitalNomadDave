﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Solution.Base.Interfaces.Model;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Interfaces.Persistance;

using System.Data.Entity;
using RefactorThis.GraphDiff;
using System.Linq.Expressions;
using System.Threading;

namespace Solution.Base.Implementation.Repository.EntityFramework

// Setting state manually is important in case of detached entities (entities loaded without change tracking or created outside of the current context).
{
    public class BaseEFRepository<TContext, TEntity> : BaseEFReadOnlyRepository<TContext, TEntity>, IBaseRepository<TEntity>
     where TContext : IBaseDbContext
     where TEntity : class, IBaseEntity, IBaseEntityAuditable, new()
    {
        public BaseEFRepository(TContext context, Boolean tracking, CancellationToken cancellationToken = default(CancellationToken))
            : base(context, tracking, cancellationToken)
        {
        }

        public virtual void CreateOrUpdate(TEntity entity, string createdModifiedBy = null)
        {
            var existingEntity = _context.Set<TEntity>().Find(entity.Id);
            if (existingEntity != null)
            {
                _context.UpdateEntity(existingEntity, entity);
                Update(existingEntity, createdModifiedBy);
            }
            else
            {
                Create(entity, createdModifiedBy);
            }
        }

        public virtual void Create(TEntity entity, string createdBy = null)
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.UserCreated = createdBy;
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity, string modifiedBy = null)
        {
           
            if (_context.IsEntityStateDetached(entity))
            {
                _context.Set<TEntity>().Attach(entity);
            }

            if (!_context.IsEntityStateAdded(entity))
            {
                entity.DateModified = DateTime.UtcNow;
                entity.UserModified = modifiedBy;
                _context.SetEntityStateModified(entity);
            }
           
            //When you change the state to Modified all the properties of the entity will be marked as modified and all the property values will be sent to the database when SaveChanges is called.
            //Note that if the entity being attached has references to other entities that are not yet tracked, then these new entities will attached to the context in the Unchanged state—they will not automatically be made Modified.If you have multiple entities that need to be marked Modified you should set the state for each of these entities individually.          
        }

        public virtual TEntity UpdateGraph(TEntity entity, Expression<Func<IUpdateConfiguration<TEntity>, object>> mapping = null, string modifiedBy = null)
        {
            entity.DateModified = DateTime.UtcNow;
            entity.UserModified = modifiedBy;
            return _context.UpdateGraph(entity, mapping);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = _context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            var dbSet = _context.Set<TEntity>();
            if (_context.IsEntityStateDetached(entity))
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        //public virtual void Save()
        //{
        //    try
        //    {
        //        context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        ThrowEnhancedValidationException(e);
        //    }
        //}

        //public virtual Task SaveAsync()
        //{
        //    try
        //    {
        //        return context.SaveChangesAsync();
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        ThrowEnhancedValidationException(e);
        //    }

        //    return Task.FromResult(0);
        //}

        //protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        //{
        //    var errorMessages = e.EntityValidationErrors
        //            .SelectMany(x => x.ValidationErrors)
        //            .Select(x => x.ErrorMessage);

        //    var fullErrorMessage = string.Join("; ", errorMessages);
        //    var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
        //    throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        //}
    }

}
