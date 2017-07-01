using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : BaseController
    {

        //403
        public ActionResult UnauthorizedAccess_403()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        //404
        public ActionResult NotFound_404()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        //500
        public ActionResult ServerError_500()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult ThrowError()
        {
            throw new NotImplementedException("Throw Error Test");
        }    

    }
}
