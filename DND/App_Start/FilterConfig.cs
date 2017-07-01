using Solution.Base.App_Start;
using System.Web;
using System.Web.Mvc;

namespace DND
{
    public class FilterConfig : BaseFilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            BaseFilterConfig.RegisterGlobalFilters(filters);
        }
    }
}
