using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Linq.Expressions;
using RefactorThis.GraphDiff;
using Microsoft.AspNet.Identity.EntityFramework;
using Solution.Base.Implementation.Models;

namespace Solution.Base.Interfaces.Persistance
{
    public interface IBaseDbContext : IDisposable
    {
        IDbSet<Faq> Faqs { get; set; }

        IDbSet<LogAction> LogActions { get; set; }
        IDbSet<ContentText> ContentText { get; set; }
        IDbSet<ContentHtml> ContentHtml { get; set; }

        IDbSet<MailingList> MailingList { get; set; }

        IDbSet<User> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        //Database Database { get; }
        IBaseDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
        DbContextConfiguration Configuration { get; }
        IDbSet<T> Set<T>() where T: class;
        DbSet Set(Type type);
        DbEntityEntry Entry(object entity);
        void UpdateEntity(object existingEntity, object newEntity);
        T UpdateGraph<T>(T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping = null) where T : class, new();
        Boolean IsEntityStateAdded(object entity);
        void SetEntityStateAdded(object entity);
        Boolean IsEntityStateDeleted(object entity);
        void SetEntityStateDeleted(object entity);
        Boolean IsEntityStateModified(object entity);
        void SetEntityStateModified(object entity);
        Boolean IsEntityStateDetached(object entity);
        void SetEntityStateDetached(object entity);
        Boolean IsEntityStateUnchanged(object entity);
        void SetEntityStateUnchanged(object entity);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        IEnumerable<TResultType> SQLQueryNoTracking<TResultType>(string query, params object[] paramaters);
        IEnumerable<TResultType> SQLQueryTracking<TResultType>(string query, params object[] paramaters) where TResultType : class;
        Task<IEnumerable<TResultType>> SQLQueryNoTrackingAsync<TResultType>(string query, params object[] paramaters);
        Task<IEnumerable<TResultType>> SQLQueryTrackingAsync<TResultType>(string query, params object[] paramaters) where TResultType : class;
    }
}
