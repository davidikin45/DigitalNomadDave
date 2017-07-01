using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Authorization
{
    public class MvcAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
                //401 - This will redirect to Login Page
                base.HandleUnauthorizedRequest(filterContext);
            else
            {
                // Authenticated, but not AUTHORIZED.  Return 403 instead!
                //filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult(403);

                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

                string url = urlHelper.Action("UnauthorizedAccess_403", "Error");

                filterContext.Result = new TransferRequestResult(url);
            }
        }
    }
}
