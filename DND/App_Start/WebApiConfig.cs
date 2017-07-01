using Solution.Base.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace DND
{
    public class WebApiConfig : BaseWebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            BaseRegister(config);
        }
    }
}
