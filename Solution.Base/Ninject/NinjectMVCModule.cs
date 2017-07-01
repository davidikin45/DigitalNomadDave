using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ninject.Modules;
using Solution.Base.Automapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject.Extensions.Conventions;
using System.Security.Principal;
using System.Web;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Web.Common;

namespace Solution.Base.Ninject
{
    public class NinjectMVCModule<TUser> : NinjectModule
        where TUser : IdentityUser, IUser
    {
        public override void Load()
        {

            Bind<IAuthenticationManager>().ToMethod(
            c =>
             HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            Bind<IUserStore<TUser>>().To<UserStore<TUser>>();
            Bind<UserManager<TUser>>().ToSelf();

            Bind<IIdentity>().ToMethod(ctx => HttpContext.Current.User.Identity);
            Bind<HttpSessionStateBase>()
              .ToMethod(m => new HttpSessionStateWrapper(HttpContext.Current.Session));
            //Bind<HttpContextBase>()
            //   .ToMethod(m => new HttpContextWrapper(HttpContext.Current));
            Bind<HttpServerUtilityBase>()
                .ToMethod(m => new HttpServerUtilityWrapper(HttpContext.Current.Server));
        }
    }
}
