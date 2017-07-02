using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Interfaces.Validation;
using Solution.Base.Implementation.Validation;
using AutoMapper;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http.Results;
using Solution.Base.Alerts;
using Solution.Base.Interfaces.Services;
using Solution.Base.Interfaces.Model;
using System.Web.Http.Description;
using Solution.Base.Helpers;
using Solution.Base.Implementation.DTOs;
using System.Web.Mvc;
using Solution.Base.Email;
using Solution.Base.ModelMetadata;

namespace Solution.Base.Controllers
{

    //Edit returns a view of the resource being edited, the Update updates the resource it self

    //C - Create - POST
    //R - Read - GET
    //U - Update - PUT
    //D - Delete - DELETE

    //If there is an attribute applied(via[HttpGet], [HttpPost], [HttpPut], [AcceptVerbs], etc), the action will accept the specified HTTP method(s).
    //If the name of the controller action starts the words "Get", "Post", "Put", "Delete", "Patch", "Options", or "Head", use the corresponding HTTP method.
    //Otherwise, the action supports the POST method.
    public abstract class BaseEntityReadOnlyController<TDto, IEntityService> : BaseController
        where TDto : class, IBaseEntity
        where IEntityService : IBaseEntityService<TDto>
    {   
        public IEntityService Service { get; private set; }
        public Boolean Admin { get; set; }

        public BaseEntityReadOnlyController(Boolean admin, IEntityService service, IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        : base(mapper,logFactory, emailService)
        {
            Admin = admin;
            Service = service;
        }

        // GET: Default
        [Route("")]
        public virtual async Task<ActionResult> Index(int page = 1, int pageSize = 10, string orderColumn = nameof(IBaseEntity.Id), string orderType = OrderByType.Descending, string search = "")
        {

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);
                  
            try
            {
                var dataTask = Service.SearchAsync(cts.Token, search, null, LamdaHelper.GetOrderBy<TDto>(orderColumn, orderType), page - 1, pageSize, null);
                var totalTask = Service.GetSearchCountAsync(cts.Token,search);

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var response = new WebApiPagedResponseDTO<TDto>
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

                ViewBag.PageTitle = Title;
                ViewBag.Admin = Admin;
                return View("List", response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        [ChildActionOnly]
        public virtual ActionResult ViewAllChild(int page = 1, int pageSize = 10, string orderColumn = nameof(IBaseEntity.Id), string orderType = OrderByType.Descending, string search = "")
        { 
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);
        
            try
            {
                IEnumerable<TDto> data = null;
                int total = 0;

                var result = Task.Run(async () => {
                    var dataTask = Service.SearchAsync(cts.Token, search, null, LamdaHelper.GetOrderBy<TDto>(orderColumn, orderType), page - 1, pageSize, null);
                    var totalTask = Service.GetSearchCountAsync(cts.Token, search);

                    await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                    data = dataTask.Result;
                    total = totalTask.Result;

                    return true;
                }).Result;

                var response = new WebApiPagedResponseDTO<TDto>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = data.ToList(),
                    OrderColumn = orderColumn,
                    OrderType = orderType,
                    Search= search
                };

                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                return PartialView("_List", data.ToList());
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        // GET: Default/Details/5
        [Route("details/{id}")]
        public virtual async Task<ActionResult> Details(object id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            TDto data = null;
            try
            {
                data = await Service.GetByIdAsync(id, cts.Token);
                if (data == null)
                    return HandleReadException();
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }

            ViewBag.PageTitle = Title;
            ViewBag.Admin = Admin;
            return View("Details", data);
        }

        [ChildActionOnly]
        public virtual ActionResult DetailsChild(object id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            TDto data = null;
            try
            {
                var result = Task.Run(async () => {
                    data = await Service.GetByIdAsync(id, cts.Token);
                    return true;
                }).Result;
                if (data == null)
                    return HandleReadException();
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
            return PartialView("_Details", data);
        }

        protected virtual TDto CreateNewDtoInstance()
        {
           return (TDto)Activator.CreateInstance(typeof(TDto));
        }

    }
}

