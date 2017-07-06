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
using Solution.Base.Interfaces.Logging;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Repository;
using System.IO;
using Solution.Base.Implementation.DTOs;
using System.Threading.Tasks;
using System;
using Solution.Base.Extensions;
using Solution.Base.ModelMetadata;
using DND.Core.Constants;
using System.Web.UI;
using System.Web;
using DND.Core.DTOs;

namespace DND.Controllers
{
    [RoutePrefix("location")]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService, IMapper mapper, ILogFactory logFactory, IFileSystemRepositoryFactory fileSystemRepositoryFactory)
             : base(mapper, logFactory)
        {
            _locationService = locationService;
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("")]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 20, string orderColumn = nameof(LocationDTO.Name), string orderType = OrderByType.Ascending, string search = "")
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            try
            {
                var dataTask = _locationService.SearchAsync(cts.Token, search, l => !string.IsNullOrEmpty(l.Album) && !string.IsNullOrEmpty(l.UrlSlug), LamdaHelper.GetOrderBy<LocationDTO>(orderColumn, orderType), (page - 1) * pageSize, pageSize);
                var totalTask = _locationService.GetSearchCountAsync(cts.Token, search, l => !string.IsNullOrEmpty(l.Album) && !string.IsNullOrEmpty(l.UrlSlug));

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var response = new WebApiPagedResponseDTO<LocationDTO>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = data.ToList(),
                    OrderColumn = orderColumn,
                    OrderType = orderType,
                    Search = search
                };

                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                return View(response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        // GET: Default/Details/5
        [Route("{urlSlug}")]
        public virtual async Task<ActionResult> Location(string urlSlug)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            try
            {
                var data = await _locationService.GetLocationAsync(urlSlug, cts.Token);

                if (data == null)
                    throw new HttpException(404, "Location not found");

                return View("Location", data);
            }
            catch (Exception ex)
            {
                if (ex is HttpException)
                {
                    throw ex;
                }
                else
                {
                    return HandleReadException();
                }
            }

        }

    }
}