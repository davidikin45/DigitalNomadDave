using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Models;
using DND.Core.Interfaces.Services;
using Solution.Base.Helpers;
using Solution.Base.ActionResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.Base.Implementation.Validation;
using System;
using DND.Core.DTOs;
using System.Threading;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Controllers.Api;
using System.Web.Http;
using System.Web.Http.Description;

namespace DND.Controllers
{
    //webapi
    //    The algorithm ASP.NET uses to calculate the "default" method for a given action goes like this:
    //If there is an attribute applied (via[HttpGet], [HttpPost], [HttpPut], [AcceptVerbs], etc), the action will accept the specified HTTP method(s).
    //If the name of the controller action starts the words "Get", "Post", "Put", "Delete", "Patch", "Options", or "Head", use the corresponding HTTP method.
    //Otherwise, the action supports the POST method.
    //https://www.exceptionnotfound.net/using-http-methods-correctly-in-asp-net-web-api/
    //in webapi  HTTP methods GET, (POST, PUT), PATCH, DELETE help the routing CRUD

    //without web api json can't be returned with GET


    //cannot begin or end with /
    [RoutePrefix("api/locale")]
    public class LocaleController : BaseWebApiController
    {
        private readonly ILocaleService _localeService;

        public LocaleController(ILocaleService localeService, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            _localeService = localeService;
        }

        [Route("")]
        [ResponseType(typeof(IList<LocaleDTO>))]
        public virtual async Task<IHttpActionResult> Get(CancellationToken cancellationToken = default(CancellationToken))
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, ClientDisconnectedToken());

            var response = await _localeService.GetAllAsync(cts.Token);

            var list = response.ToList();

            return Success(list);
        }

        [Route("{id}")]
        [ResponseType(typeof(LocaleDTO))]
        public virtual async Task<IHttpActionResult> Get(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, ClientDisconnectedToken());

            var response = await _localeService.GetAsync(id, cts.Token);

            return Success(response);
        }
    }
}