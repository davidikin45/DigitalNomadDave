using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Mvc;

namespace Solution.Base.Infrastructure
{
    //http://dirk.schuermans.me/?p=749
    public class FixedCancellationTokenModelBinder : System.Web.Mvc.IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var source = CancellationTokenSource.CreateLinkedTokenSource(default(CancellationToken),
                    controllerContext.HttpContext.Response.ClientDisconnectedToken);

            return source.Token;
        }
    }
}
