using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Interfaces.Validation;
using Solution.Base.Implementation.Validation;
using AutoMapper;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Http;
using System.Threading;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http.Results;
using Solution.Base.Alerts;
using Solution.Base.Email;

namespace Solution.Base.Controllers.Api
{


    //C - Create - POST
    //R - Read - GET
    //U - Update - PUT
    //D - Delete - DELETE

    //If there is an attribute applied(via[HttpGet], [HttpPost], [HttpPut], [AcceptVerbs], etc), the action will accept the specified HTTP method(s).
    //If the name of the controller action starts the words "Get", "Post", "Put", "Delete", "Patch", "Options", or "Head", use the corresponding HTTP method.
    //Otherwise, the action supports the POST method.
    [System.Web.Http.Authorize(Roles = "admin")]
    public abstract class BaseWebApiControllerAuthorize : BaseWebApiController
    {
        public BaseWebApiControllerAuthorize()
        {

        }

        public BaseWebApiControllerAuthorize(IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
            :base(mapper, logFactory, emailService)
        {
           
        }
    }
}

