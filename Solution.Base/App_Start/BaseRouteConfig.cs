using Solution.Base.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.App_Start
{
    public abstract class BaseRouteConfig
    {
        public static void BaseRegisterRoutes(RouteCollection routes, IList<string> publicUploadFolders)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes(new CustomDirectRouteProvider());
            routes.LowercaseUrls = true;

            //RouteExistingFiles is only validation. Static files only route to MVC if a web.config handler has been created to do so
            routes.RouteExistingFiles = true;
            routes.Add(new Route(url:"{folder}/{*pathName}",defaults: null, constraints: new RouteValueDictionary( new { pathName = @"^.*\.[^\\]+$", folder = "uploads" }), routeHandler: new ContentRouteHandler(publicUploadFolders)));


            //First matches url pattern and then substitutes in missing values. Always need Controller and Action
            routes.MapRoute(
                name: "Templates",
                url: "{feature}/Template/{name}",
                defaults: new { controller = "Template", action = "Render" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional,  }
            //);
           
            var route = new LowercaseDashedRoute(
                      url: "{controller}/{action}/{id}",
                      defaults: new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }),
                      constraints: new RouteValueDictionary(),
                      routeHandler: new MvcDashedRouteHandler(),
                      dataTokens: new RouteValueDictionary(
                      new { namespaces = new[] { "DND.Controllers", "DND.Controllers.Admin", "Solution.Base.Controllers", "Solution.Base.Controllers.Admin" } }));

            routes.Add("Default", route);
        }
    }
}
