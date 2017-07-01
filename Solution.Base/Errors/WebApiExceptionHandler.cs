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
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Solution.Base.Errors
{
    public class WebApiExceptionHandler : ExceptionHandler
    {
        private readonly HttpConfiguration _configuration;

        public WebApiExceptionHandler(HttpConfiguration config)
        {
            _configuration = config;
        }

        //It is safe to assume that ExceptionFilters will be executed before ExceptionHandlers
        //If the ExceptionFilter creates a response the ExceptionHandler would not be executed.
        public override void Handle(ExceptionHandlerContext context)
        {
            var formatters = _configuration.Formatters;
            var negotiator = _configuration.Services.GetContentNegotiator();

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            WebApiMessage messageObject = null;

            if (context.Exception is ValidationErrors)
            {
                var errors = (ValidationErrors)context.Exception;

                var errorList = new List<string>();
                foreach (var databaseValidationError in errors.Errors)
                {
                    errorList.Add(databaseValidationError.PropertyExceptionMessage);
                }

                messageObject = WebApiMessage.CreateWebApiMessage(Messages.RequestInvalid, errorList);
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (context.Exception is OperationCanceledException)
            {
                //.NET generally just doesn't send a response at all

                var errorList = new List<string>();
                errorList.Add(Messages.RequestCancelled);

                messageObject = WebApiMessage.CreateWebApiMessage(Messages.RequestCancelled, errorList);
                statusCode = HttpStatusCode.InternalServerError;
            }
            else
            {

                //{
                //  "message": "An error has occurred.",
                //  "exceptionMessage": "Exception of type 'System.Exception' was thrown.",
                //  "exceptionType": "System.Exception",
                //  "stackTrace": "   at DND.Controllers.Api.AdminController.<Posts>d__2.MoveNext() in C:\\Development\\DND\\DND\\Controllers\\Api\\AdminController.cs:line 52\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Threading.Tasks.TaskHelpersExtensions.<CastToObject>d__3`1.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Controllers.ApiControllerActionInvoker.<InvokeActionAsyncCore>d__0.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Web.Http.Filters.ActionFilterAttribute.<CallOnActionExecutedAsync>d__5.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Filters.ActionFilterAttribute.<ExecuteActionFilterAsyncCore>d__0.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Controllers.ActionFilterResult.<ExecuteAsync>d__2.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Controllers.ExceptionFilterResult.<ExecuteAsync>d__0.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Web.Http.Controllers.ExceptionFilterResult.<ExecuteAsync>d__0.MoveNext()\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.Dispatcher.HttpControllerDispatcher.<SendAsync>d__1.MoveNext()"
                //}

                var errorList = new List<string>();
                errorList.Add(Messages.UnknownError);


                messageObject = WebApiMessage.CreateWebApiMessage(Messages.UnknownError, errorList);
                statusCode = HttpStatusCode.InternalServerError;
            }

           var contentType =  negotiator.Negotiate(typeof(WebApiMessage), context.Request, formatters);
            context.Result = new NegotiatedContentResult<WebApiMessage>(statusCode, messageObject, negotiator, context.Request, formatters);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true; //your logic if you want only certain exception to be handled
        }
    }
}