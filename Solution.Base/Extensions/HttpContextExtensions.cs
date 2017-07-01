using Solution.Base.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Solution.Base.Extensions
{
    public static class HttpContextExtensions
    {

        public static T GetInstance<T>(this HttpContextBase context)
        {
            return (T)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(T));
        }

        public static IBaseDbContext Database(this HttpContextBase context)
        {
            var dbContext = context.GetInstance<IDbContextFactory>().CreateDefault();
            return dbContext;
        }

        public static TIDbContext Database<TIDbContext>(this HttpContextBase context) where TIDbContext : IBaseDbContext
        {
           var dbContext = context.GetInstance<IDbContextFactory>().Create<TIDbContext>();
           return dbContext;
        }

    }
}
