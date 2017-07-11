using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AcceptAjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        #region Properties  
        public bool Accept { get; private set; }
        #endregion

        #region Constructor  
        public AcceptAjaxRequestAttribute(bool accept)
        {
            Accept = accept;
        }
        #endregion

        #region ActionMethodSelectorAttribute Members  
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");

            bool isAjaxRequest = (controllerContext.HttpContext.Request["X-Requested-With"] == "XMLHttpRequest") || ((controllerContext.HttpContext.Request.Headers != null) && (controllerContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest"));

            return Accept == isAjaxRequest;
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        #region Fields  
        private static readonly AcceptAjaxRequestAttribute _innerAttribute = new AcceptAjaxRequestAttribute(true);
        #endregion

        #region ActionMethodSelectorAttribute Members  
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return _innerAttribute.IsValidForRequest(controllerContext, methodInfo);
        }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NoAjaxRequestAttribute : ActionMethodSelectorAttribute
    {
        #region Fields  
        private static readonly AcceptAjaxRequestAttribute _innerAttribute = new AcceptAjaxRequestAttribute(false);
        #endregion

        #region ActionMethodSelectorAttribute Members  
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return _innerAttribute.IsValidForRequest(controllerContext, methodInfo);
        }
        #endregion
    }
}
