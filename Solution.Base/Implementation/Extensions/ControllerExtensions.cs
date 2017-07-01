using Solution.Base.Interfaces.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Implementation.Extensions
{
    public static class ControllersExtensions
    {
        public static void AddValidationErrors(this ModelStateDictionary modelState, IValidationErrors errors)
        {
            foreach (var error in errors.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.PropertyExceptionMessage);
            }
        }
    }
}
