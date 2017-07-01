using Newtonsoft.Json.Serialization;
using Solution.Base.Errors;
using Solution.Base.Filters;
using Solution.Base.Infrastructure;
using Solution.Base.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.App_Start
{
    public abstract class BaseWebApiConfig
    {
        public static void BaseRegister(HttpConfiguration config)
        {
            //config.Services.Replace(typeof(IAssembliesResolver), new WebApiAssemblyResolver());

            //validate model consistently
            config.Filters.Add(new WebApiValidateModelAttribute());

            //return exceptions consistently
            //config.Filters.Add(new WebApiExceptionFilterAttribute());
            config.Services.Replace(typeof(IExceptionHandler), new WebApiExceptionHandler(config));

            config.Filters.Add(new WebApiETagAttribute());
            
            config.MapHttpAttributeRoutes(new WebApiCustomDirectRouteProvider());

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional },
            //    constraints: null,
            //    handler: new WebApiHyphenatedRouteHandler(config)
            //);

            //https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
            //By default Web Api doesn't use action names at all!
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
