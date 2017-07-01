using Solution.Base.Extensions;
using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Solution.Base.Helpers
{
    public static class BootstrapHelperExtension
    {
        public static BootstrapHelper<TModel> Bootstrap<TModel>(this HtmlHelper<TModel> helper)
        {
            return new BootstrapHelper<TModel>(helper);
        }

        public class BootstrapHelper<TModel>
        {

            private readonly HtmlHelper<TModel> _htmlHelper;


            public BootstrapHelper(HtmlHelper<TModel> helper)
            {
                _htmlHelper = helper;
            }

            public  IHtmlString BootstrapLabelFor<TProp>(Expression<Func<TModel, TProp>> property)
            {
                return _htmlHelper.LabelFor(property, new
                {
                    @class = "col-md-2 form-control-label col-form-label"
                });
            }

        }

        public static IHtmlString BootstrapLabel(this HtmlHelper helper, string propertyName)
        {
            return helper.Label(propertyName, new
            {
                @class = "col-md-2 form-control-label col-form-label"
            });
        }


    }
}