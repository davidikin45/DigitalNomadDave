using Solution.Base.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.App_Start
{
    public static class ModelBinderConfig
    {
        public static void Init()
        {
            //AsyncTimeout - Default 45 Seconds
            //ExecutionTimeout / ScriptTimeout only apply to synchronous handlers in ASP.NET. Both MVC and Web API are completely asynchronous (even if you're not using async controllers) pipelines, so ExecutionTimeout / ScriptTimeout have no effect.
            ModelBinders.Binders.Remove(typeof(CancellationToken));
            ModelBinders.Binders.Add(typeof(CancellationToken), new FixedCancellationTokenModelBinder());
            ModelBinderProviders.BinderProviders.Add(new EFModelBinderProvider());
        }
    }
}
