using Solution.Base.Interfaces.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using System.Web.Http.Controllers;
using Solution.Base.Implementation.Models;
using System.Web.Mvc;
using Solution.Base.Interfaces.Model;

namespace Solution.Base.Filters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        private IDictionary<string, object> _parameters;

        [Inject]
        public IDbContextFactory DbContextFactory { get; set; }

        public string Description { get; set; }

        public LogActionAttribute()
        {

        }

        public LogActionAttribute(string description)
        {
            Description = description;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _parameters = filterContext.ActionParameters;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (var context = DbContextFactory.CreateDefault())
            {
                 var username = filterContext.RequestContext.HttpContext.User.Identity.Name;
                var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var action = filterContext.ActionDescriptor.ActionName;
                var IP = filterContext.HttpContext.Request.UserHostAddress;
			    var	dateTime = filterContext.HttpContext.Timestamp;

                var description = Description;

                if (!string.IsNullOrEmpty(description))
                {
                    foreach (var kvp in _parameters)
                    {
                        if (!(kvp.Value is CancellationToken))
                        {
                            description = description.Replace("{" + kvp.Key + "}", kvp.Value.ToString());
                        }
                    }
                }

                context.LogActions.Add(new LogAction(username, controller, action, description, dateTime, IP));

                context.SaveChanges();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
