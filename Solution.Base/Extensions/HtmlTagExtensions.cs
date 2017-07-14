using HtmlTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Solution.Base.Extensions
{
    public static class HtmlTagExtensions
    {
        public static HtmlTag AddAttributes(this HtmlTag tag, object attributes)
        {
            var dict = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);

            return AddAttributes(tag, dict);
        }

        public static HtmlTag AddAttributes(this HtmlTag tag, RouteValueDictionary attributes)
        {
            foreach (KeyValuePair<string, object> kvp in attributes)
            {
                tag.Attr(kvp.Key, kvp.Value);
            }

            return tag;
        }
    }
}
