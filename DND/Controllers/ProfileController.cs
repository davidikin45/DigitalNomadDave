using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;

namespace FlashpackerFlights.Controllers
{
	public class ProfileController : BaseController
	{
		private readonly ApplicationUserManager _userManager;

		public ProfileController(ApplicationUserManager userManager)
		{
			_userManager = userManager;
		}

		public ActionResult Index()
		{
			var model = Mapper.Map<ProfileForm>(_userManager.FindById(User.Identity.GetUserId()));
			return View(model);
		}

		public JsonResult Update(ProfileForm form)
		{
			var user = _userManager.FindById(User.Identity.GetUserId());
			user.Email = form.EmailAddress;
			user.UserName = form.FullName;
			_userManager.Update(user);

			return BetterJsonSuccess(true);
		}
	}
}