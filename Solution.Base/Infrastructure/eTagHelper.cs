using Solution.Base.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Solution.Base.Infrastructure
{
    public class ClientCacheHelper
    {
        public static Boolean GenerateLastModifiedTag(DateTime lastModified, HttpResponseBase response, HttpRequestBase request)
        {
            var clientLastModified = request.Headers["If-Modified-Since"];

            char[] split = { ',' };
            IEnumerable<string> receivedModifiedSince = new List<string>();
            if (clientLastModified != null)
            {
                receivedModifiedSince = clientLastModified.Split(split).Select(tag => tag.Trim('"'));
            }

            if (!receivedModifiedSince.Contains(lastModified.ToUniversalTime().ToString("R")))
            {
                response.AddHeader("Last-Modified", lastModified.ToUniversalTime().ToString("R"));
                return false;
            }
            else
            {
                response.SuppressContent = true;
                response.StatusCode = (int)HttpStatusCode.NotModified;
                response.StatusDescription = "Not Modified";
                response.AddHeader("Content-Length", "0");
                return true;
            }
        }

        public static Boolean GenerateETag(byte[] data, HttpResponseBase response, HttpRequestBase request)
        {
            var eTag = CryptographyHelper.GetHash(new MemoryStream(data));
            var clienteTags = request.Headers["If-None-Match"];

            char[] split = { ',' };
            IEnumerable<string> receivedEntityTags = new List<string>();
            if (clienteTags != null)
            {
                receivedEntityTags = clienteTags.Split(split).Select(tag => tag.Trim('"'));
            }

            if (!receivedEntityTags.Contains(eTag))
            {
                response.AddHeader("ETag", "\"" + eTag + "\"");
                return false;
            }
            else
            {
                response.SuppressContent = true;
                response.StatusCode = (int)HttpStatusCode.NotModified;
                response.StatusDescription = "Not Modified";
                response.AddHeader("Content-Length", "0");
                return true;
            }
        }
    }
}
