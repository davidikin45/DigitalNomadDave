using AutoMapper;
using Solution.Base.Email;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Solution.Base.Controllers.Admin
{
    [RoutePrefix("admin/content-text")]
    public class AdminContentTextController : BaseEntityControllerAuthorize<ContentTextDTO, IContentTextService>
    {
        public AdminContentTextController(IContentTextService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
             : base(true, service, mapper, logFactory, emailService)
        {
        }


    }
}
