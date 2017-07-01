using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;
using FlashpackerFlights.Core.Model;
using FlashpackerFlights.Core.Interfaces.Services;

using Solution.Base.Extensions;

namespace FlashpackerFlights.Controllers
{
	public class OpportunityController : BaseController
	{
        private readonly IOpportunityService _opportunityService;

        public OpportunityController(IOpportunityService opportunityService)
		{
            _opportunityService = opportunityService;
		}

		public ViewResult Index()
		{
			var models = _opportunityService.GetAll().MapTo<OpportunityViewModel>().ToArray();
			return View(models);
		}

		public JsonResult Add(AddOpportunityForm form)
		{

			var opportunity = Mapper.Map<Opportunity>(form);
            _opportunityService.Create(opportunity);


           var model = Mapper.Map<CustomerOpportunityViewModel>(opportunity);

			return BetterJsonSuccess(model);
		}
	}
}