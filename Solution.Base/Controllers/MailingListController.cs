using AutoMapper;
using Solution.Base.Alerts;
using Solution.Base.Helpers;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Controllers
{
    public class MailingListController : BaseController
    {
        private readonly IMailingListService _mailingListService;

        public MailingListController(IMailingListService mailingListService, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            _mailingListService = mailingListService;
        }

        //[OutputCache(Duration = 86400, VaryByParam = "none")]
        [ChildActionOnly]
        public PartialViewResult Submit()
        {
            var dto = new MailingListDTO();
            return PartialView("_MailingList", dto);
        }

        [HttpPost]
        public ActionResult Submit(MailingListDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _mailingListService.Create(dto);
                    return PartialView("_Thankyou", dto);
                }
                catch (Exception ex)
                {
                    HandleUpdateException(ex);
                }
            }
            //error
            return PartialView("_MailingList", dto);
        }
    }
}
