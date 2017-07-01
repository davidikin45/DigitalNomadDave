
using System.Web;
using System.Web.Mvc;
using Solution.Base.Extensions;

namespace Solution.Base.Helpers
{
	public static class JsonHtmlHelpers
	{
		public static IHtmlString JsonFor<T>(this HtmlHelper helper, T obj)
		{
			return helper.Raw(obj.ToJson());
		}
	}
}