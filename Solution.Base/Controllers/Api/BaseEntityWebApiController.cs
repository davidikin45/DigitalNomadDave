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
using Solution.Base.Email;

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
    public abstract class BaseEntityWebApiController<TDto, IEntityService> : BaseEntityReadOnlyWebApiController<TDto, IEntityService>
        where TDto : class, IBaseEntity
        where IEntityService : IBaseEntityService<TDto>
    {   

        public BaseEntityWebApiController(IEntityService service, IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        : base(service, mapper, logFactory, emailService)
        {
          
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(WebApiMessage))]
        public virtual async Task<IHttpActionResult> Add(TDto dto)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var id = await Service.CreateAsync(dto, cts.Token);
            return ApiCreatedSuccessMessage(Messages.AddSuccessful, id);
        }

        [Route("update")]
        [HttpPost]
        [ResponseType(typeof(WebApiMessage))]
        public virtual async Task<IHttpActionResult> Update(TDto dto)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            await Service.UpdateAsync(dto, cts.Token);
            return ApiSuccessMessage(Messages.UpdateSuccessful, dto.Id);
        }

        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(WebApiMessage))]
        public virtual async Task<IHttpActionResult> Delete([FromBody]int id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            await Service.DeleteAsync(id, cts.Token);
            return ApiSuccessMessage(Messages.DeleteSuccessful, id);
        }

    }
}

