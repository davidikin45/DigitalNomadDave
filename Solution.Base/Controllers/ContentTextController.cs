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
    public class ContentHtmlController : BaseController
    {
        private readonly IContentHtmlService Service;

        public ContentHtmlController(IContentHtmlService service, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            Service = service;
        }

        [OutputCache(Duration = 86400, VaryByParam = "*", VaryByCustom = "CacheExpiryKey")]
        [ChildActionOnly]
        public virtual ActionResult DetailsChild(string id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            ContentHtmlDTO data = null;
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
            return PartialView("_ContentHtml", data);
        }

        [ChildActionOnly]
        public virtual ActionResult DetailsChildNoCache(string id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            ContentHtmlDTO data = null;
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
            return PartialView("_ContentHtml", data);
        }

    }
}
