using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.Routing
{
    public class MvcDashedRouteHandler : MvcRouteHandler
    {

        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)

        {

            var values = requestContext.RouteData.Values;

            values["action"] = UnDash(values["action"].ToString());

            values["controller"] = UnDash(values["controller"].ToString());

            return base.GetHttpHandler(requestContext);

        }

        private string UnDash(string path)

        {

            if (path.Length == 0)

                return path;

            var sb = new StringBuilder();

            sb.Append(Char.ToUpperInvariant(path[0]));

            for (int i = 1; i < path.Length; i++)

            {

                if (path[i] == '-')

                {
                    if (i + 1 < path.Length)

                    {
                        sb.Append(Char.ToUpperInvariant(path[i + 1]));
                        i++;
                    }

                }
                else
                {
                    sb.Append(path[i]);
                }
            }

            return sb.ToString();
        }
    }
}
