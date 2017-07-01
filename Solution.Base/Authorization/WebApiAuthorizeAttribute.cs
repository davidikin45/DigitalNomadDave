using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Solution.Base.Authorization
{ 
    ////http://stackoverflow.com/questions/20149750/owin-unauthorised-webapi-call-returning-login-page-rather-than-401
    public class WebApiAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
                base.HandleUnauthorizedRequest(actionContext);
            else
            {
                // Authenticated, but not AUTHORIZED.  Return 403 instead!
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            }
        }
    }
}
