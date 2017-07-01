using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Routing;

namespace Solution.Base.Routing
{
    public class CustomDirectRouteProvider : DefaultDirectRouteProvider
    {
        protected override IReadOnlyList<IDirectRouteFactory>
        GetActionRouteFactories(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes(typeof(IDirectRouteFactory), inherit: true).Cast<IDirectRouteFactory>().ToArray();
        }
    }
}
