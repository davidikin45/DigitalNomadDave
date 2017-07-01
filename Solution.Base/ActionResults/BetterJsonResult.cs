using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Solution.Base.Extensions;
using System.Web.Http;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Solution.Base.Alerts;

namespace Solution.Base.ActionResults
{

    public class BetterJsonResult : JsonResult
	{
        public int ErrorStatusCode = 400;
		public IList<string> Errors { get; private set; }
        public string Message { get; private set; }
        public ModelStateDictionary ModelState { get; private set; }

        public BetterJsonResult()
        {
            Errors = new List<string>();
        }

        public BetterJsonResult(string message)
        {
            Message = message;
            Errors = new List<string>();
        }

        public BetterJsonResult(string message, ModelStateDictionary modelState)
        {
            Message = message;
            Errors = new List<string>();
            ModelState = modelState;
        }

        public void AddError(string errorMessage)
		{
			Errors.Add(errorMessage);
		}

		public override void ExecuteResult(ControllerContext context)
		{
			DoUninterestingBaseClassStuff(context);

			SerializeData(context.HttpContext.Response);
		}

		private void DoUninterestingBaseClassStuff(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
			    "GET".Equals(context.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException(
					"GET access is not allowed.  Change the JsonRequestBehavior if you need GET access.");
			}

			var response = context.HttpContext.Response;
			response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}
		}

		protected virtual void SerializeData(HttpResponseBase response)
		{
			if (Errors.Any())
			{
                Data = BuildWebApiMessageObject();

                response.StatusCode = ErrorStatusCode;
			}

			if (Data == null) return;

			response.Write(Data.ToJson());
		}

        public WebApiMessage BuildWebApiMessageObject()
        {
            return WebApiMessage.CreateWebApiMessage(Message, Errors, ModelState);
        }
    }

	public class BetterJsonResult<T> : BetterJsonResult
	{
		public new T Data
		{
			get { return (T)base.Data; }
			set { base.Data = value; }
		}
	}

}