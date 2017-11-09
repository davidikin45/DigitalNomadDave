using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Web.Http;
using Newtonsoft.Json;
using Solution.Base.App_Start;
using System.Threading;
using Solution.Base.Tasks;
using Ninject;
using Solution.Base.Controllers;
using Solution.Base.Alerts;
using Solution.Base.Extensions;
using Solution.Base.Errors;
using Solution.Base.Interfaces.Logging;

namespace DND
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SQLConfig.init(Server.MapPath("~/bin/"));
            MvcConfig.Init();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ServicePointMonitor.Start(new TimeSpan(0, 0, 1), new TimeSpan(0, 10, 0), 20, 100, -1, false, false);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JsonConfig.Init();
            ModelBinderConfig.Init();
            RazorViewEngineConfig.Init();

            var taskRunner = (TaskRunner)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(TaskRunner));
            taskRunner.RunTasksAtInit();
            taskRunner.RunTasksAtStartup();
        }
        

        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "CacheExpiryKey")
            {
                object o = HttpContext.Current.Application["CacheGuid"];
                if (o == null)
                {
                    o = Guid.NewGuid();
                    HttpContext.Current.Application["CacheGuid"] = o;
                }
                return o.ToString();
            }
            return base.GetVaryByCustomString(context, arg);
        }

        protected void Application_BeginRequest()
        {
            var taskRunner = (TaskRunner)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(TaskRunner));
            taskRunner.RunTasksOnEachRequest();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var taskRunner = (TaskRunner)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(TaskRunner));
            taskRunner.RunTasksOnError();

            var httpContext = ((MvcApplication)sender).Context;
            var ex = Server.GetLastError();

            var logFactory = (ILogFactory)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(ILogFactory));

            MvcExceptionHandler.LogException(logFactory.GetLogger(), ex);
            MvcExceptionHandler.HandleException(httpContext, ex);
        }

        protected void Application_EndRequest()
        {
            var taskRunner = (TaskRunner)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(TaskRunner));
            taskRunner.RunTasksAfterEachRequest();
        }


    }
}
