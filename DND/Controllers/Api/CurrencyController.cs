using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Model;
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
using System.Web.Http;
using Solution.Base.Controllers.Api;
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
    [RoutePrefix("api/currency")]
    public class CurrencyController : BaseWebApiController
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {

            _currencyService = currencyService;
        }

        [Route("")]
        [ResponseType(typeof(List<CurrencyDTO>))]
        public virtual async Task<IHttpActionResult> Get(CancellationToken cancellationToken = default(CancellationToken))
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, ClientDisconnectedToken());

            var response = await _currencyService.GetAllAsync(cts.Token);

            var list = response.ToList();

            return Success(list);
        }

        [Route("{id:alpha}")]
        [ResponseType(typeof(CurrencyDTO))]
        public virtual async Task<IHttpActionResult> Get(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, ClientDisconnectedToken());

            var response = await _currencyService.GetAsync(id, cts.Token);

            return Success(response);
        }
    }
}