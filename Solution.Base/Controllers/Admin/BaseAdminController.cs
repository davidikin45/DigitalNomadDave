using AutoMapper;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Controllers.Admin
{
    public abstract class BaseAdminControllerAuthorize : BaseControllerAuthorize
    {

        public BaseAdminControllerAuthorize(IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("file-manager")]
        public ActionResult FileManager()
        {
            return View();
        }

        //https://stackoverflow.com/questions/565239/any-way-to-clear-flush-remove-outputcache/16038654
        [Route("clear-cache")]
        public ActionResult ClearCache()
        {
            System.Web.HttpContext.Current.Application["CacheGuid"] = Guid.NewGuid();
            //CacheHelper.ClearOutputCache();
            return View();
        }
    }
}
