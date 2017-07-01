using System.Web.Mvc;

namespace Solution.Base.Controllers
{
	public abstract class BaseTemplateController : BaseController
    {


		public PartialViewResult Render(string feature, string name)
		{
			return PartialView(string.Format("~/js/app/{0}/templates/{1}", feature, name));
		}

        
	}
}