using Solution.Base.Alerts;
using Solution.Base.Controllers;
using Solution.Base.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.Errors
{
    public static class MvcExceptionHandler
    {

        public static void HandleException(HttpContext httpContext, Exception ex)
        {
            var status = ex is HttpException ? ((HttpException)ex).GetHttpCode() : 500;
            // Is Ajax request? return json
            if (IsAjaxRequest(httpContext.Request))
            {
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;
                httpContext.Response.TrySkipIisCustomErrors = true;


                var message = WebApiMessage.CreateWebApiMessage(Messages.UnknownError, new List<string>() { Messages.UnknownError });

                if (httpContext.Request.AcceptTypes.Contains("text/xml"))
                {
                    httpContext.Response.ContentType = "text/xml";
                    httpContext.Response.Write(message.ToXml());
                }
                else
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.Write(message.ToJson());
                }

                httpContext.Response.Flush();
                httpContext.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Thread.Sleep(1);

                //httpContext.Response.End();
            }
            else
            {
                var currentController = " ";
                var currentAction = " ";
                var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

                if (currentRouteData != null)
                {
                    if (currentRouteData.Values["controller"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                    {
                        currentController = currentRouteData.Values["controller"].ToString();
                    }

                    if (currentRouteData.Values["action"] != null &&
                        !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                    {
                        currentAction = currentRouteData.Values["action"].ToString();
                    }
                }

                var controller = new ErrorController();
                var routeData = new RouteData();

                // Server.ClearError() so as to convey to ASP.NET that the exception has been handled and that there is no longer an exception in the application. This way if you have set a custom error page in the web.config, it won't be displayed. 
                httpContext.ClearError();
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = status;

                // Certain versions of IIS will sometimes use their own error page when
                // they detect a server error. Setting this property indicates that we
                // want it to try to render ASP.NET MVC's error page instead
                httpContext.Response.TrySkipIisCustomErrors = true;

                routeData.Values["controller"] = "Error";

                switch (status)
                {
                    case 404:
                        routeData.Values["action"] = "NotFound_404";
                        break;
                    default:
                        routeData.Values["action"] = "ServerError_500";
                        break;
                }

                controller.ViewData.Model = new HandleErrorInfo(ex, currentController, currentAction);
                ((IController)controller).Execute(new RequestContext(new HttpContextWrapper(httpContext), routeData));
            }
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            var headers = request.Headers;
            if ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            return false;
        }
    }
}
