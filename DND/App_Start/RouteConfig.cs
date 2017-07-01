using Solution.Base.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DND
{
    public class RouteConfig : BaseRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            char[] seperator = { ',' };
            List<string> publicUploadFolders = System.Configuration.ConfigurationManager.AppSettings["PublicUploadFolders"].Split(seperator).ToList();
            BaseRegisterRoutes(routes, publicUploadFolders);
        }
    }
}
