using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Solution.Base.Controllers.Api
{
    [RoutePrefix("api/test")]
    public class TestController : BaseWebApiControllerAuthorize
    {
        [Route("checkid/{id}")]
        [HttpGet]
        public IHttpActionResult CheckId(int id)
        {
            if (id > 100)
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("We cannot use IDs greater than 100.")
                };
                throw new HttpResponseException(message);
            }
            return Ok(id);
        }

        [Route("unauthorized")]
        [HttpGet]
        public IHttpActionResult Unauthorized_401()
        {
            return Unauthorized();
        }

        [Route("forbidden")]
        [HttpGet]
        public IHttpActionResult Forbidden_403()
        {
            return Forbidden();
        }

        [Route("ok")]
        [HttpGet]
        public IHttpActionResult OK_200()
        {
            return Ok();
        }

        [Route("not-found")]
        [HttpGet]
        public IHttpActionResult NotFound_404()
        {
            return NotFound();
        }

        [Route("throw-exception")]
        [HttpGet]
        public IHttpActionResult ThrowException()
        {
            throw new Exception("");
        }
    }
}
