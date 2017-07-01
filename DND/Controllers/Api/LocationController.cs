using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using FlashpackerFlights.Core.ViewModels;
using FlashpackerFlights.Core.Model;
using FlashpackerFlights.Core.Interfaces.Services;
using Solution.Base.Helpers;
using Solution.Base.ActionResults;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solution.Base.Implementation.Validation;
using System;
using FlashpackerFlights.Core.DTOs;
using System.Threading;
using System.Net;
using Solution.Base.Interfaces.Logging;

namespace FlashpackerFlights.Controllers
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
    [RoutePrefix("api/v1.0/location")]
    public class LocationController : BaseController
    {
        private readonly IFlightSearchService _flightSearchService;

        public LocationController(IFlightSearchService flightSearchService, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {

            _flightSearchService = flightSearchService;
        }


        [Route("")]
        public async Task<JsonResult> GetByID(LocationForm requestForm, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (!ModelState.IsValid) //Because form object implements IValidatableObject the validation has already occured!
                    return ValidationErrors(ModelState);
                //return BetterJsonSuccess(requestForm.Validate());
                var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, HttpContext.Response.ClientDisconnectedToken);

                var request = Mapper.Map<LocationForm, LocationRequestDTO>(requestForm);
                var response = await _flightSearchService.GetLocationAsync(request, cts.Token);

                return BetterJsonAllowGetSuccess(response);

            }
            catch (ValidationErrors errors)
            {
                return ValidationErrors(errors);
            }
            catch (OperationCanceledException ex)
            {
                return OperationCancelledError(ex);
            }
            catch (Exception ex)
            {
                return OtherError(ex);
            }
        }

        [Route("auto-suggest")]
        public async Task<JsonResult> LocationAutoSuggest(LocationAutoSuggestForm requestForm, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (!ModelState.IsValid) //Because form object implements IValidatableObject the validation has already occured!
                    return ValidationErrors(ModelState);

                //return BetterJsonSuccess(requestForm.Validate());
                var cts = TaskHelper.CreateChildCancellationTokenSource(cancellationToken, HttpContext.Response.ClientDisconnectedToken);
                var request = Mapper.Map<LocationAutoSuggestForm, LocationAutoSuggestRequestDTO>(requestForm);
                var response = await _flightSearchService.LocationAutoSuggestAsync(request, cts.Token);

                return BetterJsonAllowGetSuccess(response);

            }
            catch (ValidationErrors errors)
            {
                return ValidationErrors(errors);
            }
            catch (OperationCanceledException ex)
            {
                return OperationCancelledError(ex);
            }
            catch (Exception ex)
            {
                return OtherError(ex);
            }
        }
    }
}