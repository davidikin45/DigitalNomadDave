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
    public abstract class BaseWebApiController : ApiController
    {
        public IMapper Mapper { get; }
        public ILogFactory LogFactory { get; }
        public ILogger Logger { get; }
        public IEmailService EmailService { get; }

        public BaseWebApiController()
        {

        }

        public BaseWebApiController(IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        {
            Mapper = mapper;
            LogFactory = logFactory;
            if (logFactory != null)
            {
                Logger = LogFactory.GetLogger();
            }
            EmailService = emailService;
        }

        //protected virtual IHttpActionResult BetterJsonError(string message, ModelStateDictionary modelState)
        //{
        //    var errors = modelState.Values.SelectMany(v => v.Errors);
        //    var errorList = new List<string>();
        //    foreach (var error in errors)
        //    {
        //        errorList.Add(error.ErrorMessage);
        //    }

        //    var WebApiMessage = WebApiMessage.CreateWebApiMessage(message, errorList, modelState);

        //    return Content(HttpStatusCode.BadRequest, WebApiMessage);
        //}

        //protected virtual IHttpActionResult BetterJsonError(string message, ValidationErrors errors, int errorStatusCode = 400)
        //{
        //    var errorList = new List<string>();
        //    foreach (var databaseValidationError in errors.Errors)
        //    {
        //        errorList.Add(databaseValidationError.PropertyExceptionMessage);
        //    }

        //    var WebApiMessage = WebApiMessage.CreateWebApiMessage(message, errorList);

        //    return Content((HttpStatusCode)errorStatusCode, WebApiMessage);
        //}

        protected virtual IHttpActionResult Success<T>(T model)
        {
            return Content(HttpStatusCode.OK, model);
        }

        [Obsolete("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.")]
        protected new JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException("Do not use the standard Json helpers to return JSON data to the client.  Use either JsonSuccess or JsonError instead.");
        }

        protected CancellationToken ClientDisconnectedToken()
        {
            return HttpContext.Current.Response.ClientDisconnectedToken;
        }

        protected IHttpActionResult ApiErrorMessage(string message)
        {
            return ApiErrorMessage("The request is invalid.", message);
        }

        protected virtual IHttpActionResult ApiErrorMessage(string message, string errorMessage, int errorStatusCode = 400)
        {
            var errorList = new List<string>();
            errorList.Add(errorMessage);

            var response = WebApiMessage.CreateWebApiMessage(message, errorList);

            return Content((HttpStatusCode)errorStatusCode, response);
        }

        protected virtual IHttpActionResult ApiCreatedSuccessMessage(string message, Object id)
        {
            return ApiSuccessMessage(message, id, HttpStatusCode.Created);
        }

        protected virtual IHttpActionResult ApiSuccessMessage(string message, Object id, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var errorList = new List<string>();

            var response = WebApiMessage.CreateWebApiMessage(message, errorList, id);

            return Content(statusCode, response);
        }

        protected virtual IHttpActionResult Html(string html)
        {
            return new HTMLActionResult(html);
        }


        protected virtual IHttpActionResult Forbidden()
        {
            return new WebApiForbiddenResult(this);
        }

    }
}

