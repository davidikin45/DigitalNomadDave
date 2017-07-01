using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.Routing
{
    public class WebApiHyphenatedRouteHandler : HttpControllerDispatcher
    {
        public WebApiHyphenatedRouteHandler(HttpConfiguration configuration):base(configuration)
        {
        }

        protected override Task<HttpResponseMessage>  SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IHttpRouteData routeData = request.GetRouteData();
            routeData.Values["controller"] =
              routeData.Values["controller"].ToString().Replace("-", "");
            routeData.Values["action"] =
               routeData.Values["action"].ToString().Replace("-", "");

            return base.SendAsync(request, cancellationToken);
        }
    }
}
