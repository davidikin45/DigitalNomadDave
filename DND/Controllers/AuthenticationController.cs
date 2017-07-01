using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Web.Mvc;
using Solution.Base.Controllers;
using DND;
using DND.Controllers;
using DND.Core.ViewModels;

namespace DND.Controllers
{
	[AllowAnonymous]
	public class AuthenticationController : BaseController
	{
		private readonly ApplicationUserManager _userManager;
		private readonly IAuthenticationManager _authManager;

		public AuthenticationController(ApplicationUserManager userManager, IAuthenticationManager authManager)
		{
			_userManager = userManager;
			_authManager = authManager;
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Login(LoginForm form)
		{
			var user = await _userManager.FindByEmailAsync(form.EmailAddress);

			if (user == null || ! (await _userManager.CheckPasswordAsync(user, form.Password)))
			{
				Response.StatusCode = 400;
				return Json("The username or password is invalid.");
			}


			var identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

			_authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, identity);

			return Json(true);
		}

		public ActionResult Logout()
		{
			_authManager.SignOut();

			return this.RedirectToAction<HomeController>(c => c.Index());
		}
	}
}