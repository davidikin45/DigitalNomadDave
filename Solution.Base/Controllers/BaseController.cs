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
using System.Threading;
using Solution.Base.Alerts;
using Solution.Base.Email;

namespace Solution.Base.Controllers
{
    public abstract class BaseController : Controller

    {
        public IMapper Mapper { get; }
        public ILogFactory LogFactory { get; }
        public ILogger Logger { get; }
        public IEmailService EmailService { get; }

        public BaseController()
        {

        }

        public BaseController(IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        {
            Mapper = mapper;
            LogFactory = logFactory;
            if (logFactory != null)
            {
                Logger = LogFactory.GetLogger();
            }
            EmailService = emailService;
        }

        protected virtual BetterJsonResult BetterJsonError(string message, ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);

            var JsonResult = new BetterJsonResult(message, modelState);

            foreach (var error in errors)
            {
                JsonResult.AddError(error.ErrorMessage);
            }

            return JsonResult;
        }

        protected virtual BetterJsonResult BetterJsonError(string message, ValidationErrors errors, int errorStatusCode = 400)
        {
            var JsonResult = new BetterJsonResult(message);
            JsonResult.ErrorStatusCode = errorStatusCode;
            foreach (var databaseValidationError in errors.Errors)
            {
                JsonResult.AddError(databaseValidationError.PropertyExceptionMessage);
            }

            return JsonResult;
        }

        protected virtual BetterJsonResult BetterJsonError(string message, string errorMessage, int errorStatusCode = 400)
        {
            var JsonResult = new BetterJsonResult(message);
            JsonResult.ErrorStatusCode = errorStatusCode;
            JsonResult.AddError(errorMessage);

            return JsonResult;
        }

        protected virtual BetterJsonResult BetterJsonAllowGetError(string message, ModelStateDictionary modelState, int errorStatusCode = 400)
        {
            var errors = modelState.Values.SelectMany(v => v.Errors);

            var JsonResult = new BetterJsonResult(message, modelState);
            JsonResult.ErrorStatusCode = errorStatusCode;
            JsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            foreach (var error in errors)
            {
                JsonResult.AddError(error.ErrorMessage);
            }

            return JsonResult;
        }

        protected virtual BetterJsonResult BetterJsonAllowGetError(string message, ValidationErrors errors, int errorStatusCode = 400)
        {
            var JsonResult = new BetterJsonResult(message);
            JsonResult.ErrorStatusCode = errorStatusCode;
            JsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            foreach (var databaseValidationError in errors.Errors)
            {
                JsonResult.AddError(databaseValidationError.PropertyExceptionMessage);
            }

            return JsonResult;
        }

        protected virtual BetterJsonResult BetterJsonAllowGetError(string message, string errorMessage, int errorStatusCode = 400)
        {
            var JsonResult = new BetterJsonResult(message);
            JsonResult.ErrorStatusCode = errorStatusCode;
            JsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            JsonResult.AddError(errorMessage);

            return JsonResult;
        }

        protected virtual BetterJsonResult<T> BetterJsonSuccess<T>(T model)
        {
            return new BetterJsonResult<T>() { Data = model };
        }

        protected virtual BetterJsonResult<T> BetterJsonAllowGetSuccess<T>(T model)
        {
            return new BetterJsonResult<T>() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        [Obsolete("Do not use the standard Json helpers to return JSON data to the client.  Use either BetterJsonSuccess or BetterJsonError instead.")]
        protected JsonResult Json<T>(T data)
        {
            throw new InvalidOperationException("Do not use the standard Json helpers to return JSON data to the client.  Use either BetterJsonSuccess or BetterJsonError instead.");
        }

        protected CancellationToken ClientDisconnectedToken()
        {
            return HttpContext.Response.ClientDisconnectedToken;
        }
        #region "Errors"

        public BetterJsonResult Error(string message)
        {
            return BetterJsonAllowGetError("The request is invalid.", message);
        }

        public BetterJsonResult ValidationErrors(ModelStateDictionary modelState)
        {
            return BetterJsonAllowGetError("The request is invalid.", modelState);
        }

        public BetterJsonResult ValidationErrors(ValidationErrors errors)
        {
            return BetterJsonAllowGetError("The request is invalid.", errors);
        }

        public BetterJsonResult OperationCancelledError(OperationCanceledException ex)
        {
            //Logger.Error(ex, "The request was cancelled.");
            return BetterJsonAllowGetError("The request was cancelled.", "The request was cancelled.", (int)HttpStatusCode.InternalServerError);
        }

        public BetterJsonResult OtherError(Exception ex)
        {
            Logger.Error(ex, Messages.UnknownError);
            return BetterJsonAllowGetError(Messages.UnknownError, Messages.UnknownError, (int)HttpStatusCode.InternalServerError);
        }
        #endregion

        protected ActionResult HandleReadException()
        {
            return RedirectToControllerDefault().WithError(Messages.RequestInvalid);
        }

        protected void HandleUpdateException(Exception ex)
        {
            if (ex is ValidationErrors)
            {
                var propertyErrors = (ValidationErrors)ex;
                ModelState.AddValidationErrors(propertyErrors);
            }
            else
            {
                ModelState.AddModelError("", Messages.UnknownError);
            }
        }

        protected virtual ActionResult RedirectToHome()
        {
            return RedirectToRoute("Default");
        }

        protected virtual ActionResult RedirectToControllerDefault()
        {
            return RedirectToAction("Index");
        }

        protected string ControllerName
        {
            get { return this.ControllerContext.RouteData.Values["controller"].ToString(); }
        }

        protected string Title
        {
            get
            {
                var val = ((System.Web.Routing.Route)this.ControllerContext.RouteData.Route).Url;
                return val;
            }
        }

    }
}
