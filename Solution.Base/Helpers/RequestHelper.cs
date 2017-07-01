using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Helpers
{
    public static class RequestHelper
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string szRemoteAddr = context.Request.UserHostAddress;

            return szRemoteAddr;
        }
    }
}
