using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;
using FlashpackerFlights.Core.Model;
using FlashpackerFlights.Core.Interfaces.Services;

using Solution.Base.Extensions;
using Solution.Base.ActionResults;

namespace FlashpackerFlights.Controllers
{
	public class CustomerController : BaseController
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		public ActionResult Index()
		{
			return View();
		}

        [AllowJsonGet]
		public JsonResult GetAll()
		{
			var customerModels = _customerService.Get(null, q => q.OrderByDescending(x => x.DateCreated)).MapTo<CustomerViewModel>().ToArray();
			return BetterJsonSuccess(customerModels);
		}

		public JsonResult Add(AddCustomerForm form)
		{
			var customer = Mapper.Map<Customer>(form);

			_customerService.Create(customer);

			var model = Mapper.Map<CustomerViewModel>(customer);
			return BetterJsonSuccess(model);
		}

		public JsonResult Update(EditCustomerForm form)
		{
			var target = _customerService.GetById(form.Id);

			Mapper.Map(form, target);

			_customerService.Update(target);

			var updatedCustomer = _customerService.GetById(form.Id).MapTo<CustomerViewModel>();

			return BetterJsonSuccess(updatedCustomer);
		}
	}
}