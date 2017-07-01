using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Persistance;

using Solution.Base.Extensions;
using FlashpackerFlights.Core.Interfaces.Services;

namespace FlashpackerFlights.Controllers
{
	public class RiskController : BaseController
	{
		private readonly IRiskService _riskService;

		public RiskController(IRiskService riskService)
		{
            _riskService = riskService;
		}

		public ViewResult Index()
		{
            var models = _riskService.GetAll().MapTo<RiskViewModel>().ToArray();
            return View(models);
		}

		public JsonResult Add(AddRiskForm form)
		{
            var risk = Mapper.Map<Risk>(form);

            _riskService.Create(risk);

			var model = Mapper.Map<CustomerRiskViewModel>(risk);

			return BetterJsonSuccess(model);
		}
	}
}