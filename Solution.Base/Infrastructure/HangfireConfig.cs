using Owin;
using Solution.Base.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using System.Web;

namespace Solution.Base.Infrastructure
{
    public class HangfireConfig : IRunAtOwinStartup
    {
        public void Execute(IAppBuilder app, string nameOrConnectionString)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(nameOrConnectionString);
            app.UseHangfireDashboard("/admin/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationfilter() },
                AppPath = VirtualPathUtility.ToAbsolute("~/admin")
            });
            app.UseHangfireServer();
        }
    }

    public class HangfireAuthorizationfilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            return context.Request.LocalIpAddress == "127.0.0.1" || context.Request.LocalIpAddress == "::1" || owinContext.Authentication.User.IsInRole("admin");
        }

    }
}
