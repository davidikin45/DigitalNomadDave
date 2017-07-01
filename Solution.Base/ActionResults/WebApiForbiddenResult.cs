using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Solution.Base.ActionResults
{
    public class WebApiForbiddenResult : StatusCodeResult
    {
        public WebApiForbiddenResult(HttpRequestMessage request)
            :base(HttpStatusCode.Forbidden, request)
        {

        }

        public WebApiForbiddenResult(ApiController controller)
           : base(HttpStatusCode.Forbidden, controller)
        {

        }
    }
}
