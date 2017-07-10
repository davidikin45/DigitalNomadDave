using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Models;
using Solution.Base.Interfaces.Persistance;

using DND.Core.Interfaces.Services;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Email;
using Solution.Base.Implementation.DTOs;
using DND.Core.DTOs;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace DND.Controllers
{
    [RoutePrefix("travel-map")]
	public class TravelMapController : BaseController
	{
        private readonly ILocationService Service;

        public TravelMapController(ILocationService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            :base(mapper, logFactory, emailService)
		{
            Service = service;
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("")]
		public async Task<ActionResult> Index()
		{
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            try
            {
                var dataTask = Service.GetAsync(cts.Token, l => l.ShowOnTravelMap == true, null, null, null);
                var totalTask = Service.GetCountAsync(cts.Token, l => l.ShowOnTravelMap);

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var response = new WebApiPagedResponseDTO<LocationDTO>
                {
                    Page = 1,
                    PageSize = total,
                    Records = total,
                    Rows = data.ToList(),
                    OrderColumn = "",
                    OrderType = ""
                };

                ViewBag.Page = 1;
                ViewBag.PageSize = total;

                return View(response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }
		
	}
}