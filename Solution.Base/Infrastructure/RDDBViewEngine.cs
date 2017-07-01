using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Infrastructure
{
    public class RDDBViewEngine : RazorViewEngine
    {
        private static readonly string[] NewViewFormats =
       {
        "~/Views/Shared/CRUD/{0}.cshtml",
        };

        private static readonly string[] NewPartialViewFormats =
        {
        "~/Views/Shared/CRUD/{0}.cshtml",
        "~/Views/Shared/Navigation/{0}.cshtml",
        "~/Views/Shared/Sidebar/{0}.cshtml",
         "~/Views/Shared/Footer/{0}.cshtml",
         "~/Views/Shared/Alerts/{0}.cshtml"
        };

        public RDDBViewEngine()
        {
            base.PartialViewLocationFormats = base.PartialViewLocationFormats.Union(NewPartialViewFormats).ToArray();
            base.ViewLocationFormats = base.ViewLocationFormats.Union(NewViewFormats).ToArray();
        }

    }
}
