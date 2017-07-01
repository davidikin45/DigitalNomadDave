using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.App_Start
{
    public static class MvcConfig
    {
        public static void Init()
        {
            MvcHandler.DisableMvcResponseHeader = true;
        }
    }
}
