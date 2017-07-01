using Solution.Base.Helpers;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Solution.Base.Filters
{
    public class WebApiETagAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {

        private IEnumerable<string> _receivedEntityTags;
        private bool validRequest = false;

        private readonly HttpMethod[] _supportedRequestMethods = {
            HttpMethod.Get,
            HttpMethod.Head
         };


        public override void OnActionExecuting(HttpActionContext context)
        {
            //Get the request
            var request = context.Request;
            if (_supportedRequestMethods.Contains(request.Method))
            {
                validRequest = true;
                //Get if If-None match header
                var clientEtags = request.Headers.IfNoneMatch;

                if (clientEtags != null)
                {
                    _receivedEntityTags = clientEtags.Select(t => t.Tag.Trim('"'));
                }
            }

        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            if (!validRequest || context.Response == null) return;

            var objectContent = context.Response.Content as ObjectContent;

            if (objectContent == null) return;

            var responseMessage = (context.Response.Content.ReadAsByteArrayAsync()).Result;

            var eTag = CryptographyHelper.GetHash(responseMessage);

            if (_receivedEntityTags.Contains(eTag))
            {
                context.Response.StatusCode = HttpStatusCode.NotModified;
                context.Response.Content = null;
            }

            EntityTagHeaderValue eTagHeader;

            var valid = EntityTagHeaderValue.TryParse("\"" + eTag + "\"", out eTagHeader);

            context.Response.Headers.ETag = eTagHeader;
            //SetCacheControl(context.Response);
        }

        private void SetCacheControl(HttpResponseMessage httpResponseMessage)
        {
            httpResponseMessage.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromSeconds(6),
                MustRevalidate = true,
                Private = true
            };
        }
    }
}
