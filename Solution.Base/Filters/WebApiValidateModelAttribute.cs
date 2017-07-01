using Solution.Base.ActionResults;
using Solution.Base.Alerts;
using Solution.Base.Implementation.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Solution.Base.Filters
{
    public class WebApiValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                var errors = actionContext.ModelState.Values.SelectMany(v => v.Errors);

                var errorList = new List<string>();
                foreach (var error in errors)
                {
                    errorList.Add(error.ErrorMessage);
                }

                var messageObject = WebApiMessage.CreateWebApiMessage(Messages.RequestInvalid, errorList, actionContext.ModelState);
                var negotiatedContentResult = new NegotiatedContentResult<WebApiMessage>(HttpStatusCode.BadRequest, messageObject, (ApiController)actionContext.ControllerContext.Controller);
                var responseTask = negotiatedContentResult.ExecuteAsync(default(CancellationToken));
                responseTask.Wait();
                var response = responseTask.Result;
                actionContext.Response = response;
            }
        }
    }
}