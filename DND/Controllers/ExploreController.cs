using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Model;
using Solution.Base.Interfaces.Persistance;

using DND.Core.Interfaces.Services;

namespace DND.Controllers
{
	public class ExploreController : BaseController
	{

		public ExploreController()
		{

		}

		public ViewResult Index()
		{

            return View();
		}
		
	}
}