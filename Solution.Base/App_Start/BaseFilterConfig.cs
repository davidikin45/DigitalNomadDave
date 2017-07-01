using Solution.Base.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.App_Start
{
    public class BaseFilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, Boolean cacheEverything = false)
        {
            //http://stackoverflow.com/questions/11851328/how-is-error-cshtml-called-in-asp-net-mvc
            //HandleErrorAttribute only handles 500 internal server errors
            filters.Add(new HandleErrorAttribute()); 

            filters.Add(new ETagAttribute());
        }
    }
}
