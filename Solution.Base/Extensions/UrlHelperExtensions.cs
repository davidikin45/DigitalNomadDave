using Solution.Base.Helpers;
using Solution.Base.Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Content(this UrlHelper urlHelper, string path, bool toAbsolute)
        {
            var absoluteVirtual = urlHelper.Content(path);

            Uri url = new Uri(new Uri(System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]), absoluteVirtual);

            return toAbsolute ? url.AbsoluteUri : absoluteVirtual;
        }

        public static string Content(this UrlHelper urlHelper, string folderId, string path, bool toAbsolute)
        {
            var physicalPath = HttpContext.Current.Server.GetFolderPhysicalPathById(folderId) + path;
            var absoluteVirtual = physicalPath.GetAbsoluteVirtualPath();

            Uri url = new Uri(new Uri(System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]), absoluteVirtual);

            return toAbsolute ? url.AbsoluteUri : absoluteVirtual;
        }

        public static string AbsoluteRouteUrl(
            this UrlHelper urlHelper,
            string routeName,
            object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }

        public static string AbsoluteUrl<TController>(this UrlHelper url, Expression<Action<TController>> expression, bool passRouteValues = true) where TController : Controller
        {
            string absoluteUrl = "";
            var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(expression);
            if (passRouteValues)
            {
                absoluteUrl = AbsoluteUrl(url, routeValues["Action"].ToString(), routeValues["Controller"].ToString(), routeValues);
            }
            else
            {
                absoluteUrl = AbsoluteUrl(url, routeValues["Action"].ToString(), routeValues["Controller"].ToString());
            }

            return absoluteUrl;
        }

        public static string AbsoluteUrl(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            return AbsoluteUrl(url ,actionName, controllerName, new RouteValueDictionary(routeValues));
        }

        public static string AbsoluteUrl(this UrlHelper url, string actionName, string controllerName, RouteValueDictionary routeValues = null)
        {
            var absoluteUrl = "";
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]))
            {
                absoluteUrl = string.Format("{0}{1}", System.Configuration.ConfigurationManager.AppSettings["SiteUrl"], url.Action(actionName, controllerName, routeValues));
            }
            else {
                absoluteUrl = url.Action(actionName, controllerName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme).ToString();
            }
            return absoluteUrl;
        }

        public static string Action<TController>(this UrlHelper url, Expression<Action<TController>> expression) where TController : Controller
        {
            var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(expression);
            return url.Action(routeValues["Action"].ToString(), routeValues["Controller"].ToString(), routeValues);
        }

        public static string RouteUrl<TController>(this UrlHelper url, Expression<Action<TController>> expression) where TController : Controller
        {
            var routeValues = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression<TController>(expression);
            return url.RouteUrl(routeValues);
        }

    }

}
