using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;

using Solution.Base.Extensions;
using FlashpackerFlights.Core.Interfaces.Services;

namespace FlashpackerFlights.Controllers
{
	public class ReportController : BaseController
	{
        private readonly ICustomerService _customerService;

        public ReportController(ICustomerService customerService)
		{
            _customerService = customerService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public JsonResult NewCustomers()
		{
			var startOfMonth = DateTime.Today.ToStartOfMonth();
			var endOfMonth = DateTime.Today.ToEndOfMonth();

            var customers = _customerService.Get(x => x.DateCreated >= startOfMonth && x.DateCreated <= endOfMonth).MapTo<NewCustomerReportViewModel>().ToArray();
            
			return BetterJsonSuccess(customers);
		}

		public JsonResult LostCustomers()
		{
			var startOfMonth = DateTime.Today.ToStartOfMonth();
			var endOfMonth = DateTime.Today.ToEndOfMonth();

			var customers = _customerService.Get(x => x.TerminationDate >= startOfMonth && x.TerminationDate <= endOfMonth).MapTo<NewCustomerReportViewModel>().ToArray();

            return BetterJsonSuccess(customers);
		}
	}
}