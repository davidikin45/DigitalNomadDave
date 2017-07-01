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
using System.Web.Http;
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
using Solution.Base.Email;
using HtmlTags;
using Solution.Base.Extensions;

namespace Solution.Base.Controllers.Api
{

    //Edit returns a view of the resource being edited, the Update updates the resource it self

    //C - Create - POST
    //R - Read - GET
    //U - Update - PUT
    //D - Delete - DELETE

    //If there is an attribute applied(via[HttpGet], [HttpPost], [HttpPut], [AcceptVerbs], etc), the action will accept the specified HTTP method(s).
    //If the name of the controller action starts the words "Get", "Post", "Put", "Delete", "Patch", "Options", or "Head", use the corresponding HTTP method.
    //Otherwise, the action supports the POST method.
    public abstract class BaseEntityReadOnlyWebApiController<TDto, IEntityService> : BaseWebApiController
        where TDto : class, IBaseEntity
        where IEntityService : IBaseEntityService<TDto>
    {   
        public IEntityService Service { get; private set; }

        public BaseEntityReadOnlyWebApiController(IEntityService service, IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        : base(mapper,logFactory, emailService)
        {
            Service = service;
        }

        [Route("{id}"), Route("get/{id}")]
        [HttpGet]
        [ResponseType(typeof(IBaseEntity))]
        public virtual async Task<IHttpActionResult> Get(int id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

           var response =  await Service.GetByIdAsync(id, cts.Token);
           return Success(response);
        }

        [Route("get-all")]
        [HttpGet]
        [ResponseType(typeof(List<IBaseEntity>))]
        public virtual async Task<IHttpActionResult> GetAll()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var response = await Service.GetAllAsync(cts.Token);

            var list = response.ToList();

            return Success(list);
        }

        [Route("get-paged")]
        [HttpPost]
        [ResponseType(typeof(WebApiPagedResponseDTO<IBaseEntity>))]
        public virtual async Task<IHttpActionResult> GetPaged(WebApiPagedRequestDTO jqParams)
        {
            if (string.IsNullOrEmpty(jqParams.OrderBy))
                jqParams.OrderBy = "id";

            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var dataTask = Service.GetAllAsync(cts.Token, LamdaHelper.GetOrderBy<TDto>(jqParams.OrderBy, jqParams.OrderType),jqParams.Page - 1, jqParams.PageSize);

            var totalTask = Service.GetCountAsync(cts.Token);

            await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

            var data = dataTask.Result;
            var total = totalTask.Result;

            var response = new WebApiPagedResponseDTO<TDto>
            {
                Page = jqParams.Page,
                PageSize = jqParams.PageSize,
                Records = total,
                Rows = data.ToList()
            };

            return Success(response);
        }


        [Route("get-all-paged")]
        [HttpGet]
        [ResponseType(typeof(WebApiPagedResponseDTO<IBaseEntity>))]
        public virtual async Task<IHttpActionResult> GetAllPaged()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var dataTask = Service.GetAllAsync(cts.Token);

            var totalTask = Service.GetCountAsync(cts.Token);

            await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

            var data = dataTask.Result;
            var total = totalTask.Result;

            var response = new WebApiPagedResponseDTO<TDto>
            {
                Page = 1,
                PageSize = total,
                Records = total,
                Rows = data.ToList()
            };

            return Success(response);
        }

        /// <summary>
        /// Tagses the HTML.
        /// </summary>
        /// <returns></returns>
        [Route("get-all-html")]
        [HttpGet]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> GetAllHtml()
        {

            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var data = await Service.GetAllAsync(cts.Token);

            var select = new HtmlTag("select");

            foreach (var item in data)
            {
                var description = item.Id.ToString();
                
                if (item.HasProperty("Name") && !string.IsNullOrEmpty((string)item.GetPropValue("Name")))
                {
                    description = (string)item.GetPropValue("Name");
                }

                select.Append(new HtmlTag("option").Text(description).Value(item.Id.ToString()));
            }

            return Html(select.ToString());
        }

    }
}

