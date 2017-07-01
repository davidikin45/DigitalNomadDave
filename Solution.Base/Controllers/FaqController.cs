using AutoMapper;
using Solution.Base.Alerts;
using Solution.Base.Helpers;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Services;
using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Controllers
{
    public class FaqController : BaseController
    {
        private readonly IFaqService Service;

        public FaqController(IFaqService service, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            Service = service;
        }

        [OutputCache(Duration = 86400, VaryByParam = "None", VaryByCustom = "CacheExpiryKey")]
        [ChildActionOnly]
        public virtual ActionResult ViewAll()
        {

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            try
            {
                IEnumerable<FaqDTO> data = null;
                int total = 0;

                var result = Task.Run(async () => {
                    var dataTask = Service.GetAllAsync(cts.Token, LamdaHelper.GetOrderBy<FaqDTO>(nameof(FaqDTO.DateCreated), OrderByType.Ascending), null, null, null);
                    var totalTask = Service.GetCountAsync(cts.Token);

                    await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                    data = dataTask.Result;
                    total = totalTask.Result;

                    return true;
                }).Result;

                var response = new WebApiPagedResponseDTO<FaqDTO>
                {
                    Page = 1,
                    PageSize = total,
                    Records = total,
                    Rows = data.ToList()
                };

                ViewBag.Page = 1;
                ViewBag.PageSize = total;

                return PartialView("_Faq", response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

    }
}
