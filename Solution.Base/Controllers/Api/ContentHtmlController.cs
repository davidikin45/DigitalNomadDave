﻿using AutoMapper;
using Solution.Base.Email;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Solution.Base.Controllers.Api
{
    [RoutePrefix("api/content-html")]
    public class ContentHtmlController : BaseEntityWebApiControllerAuthorize<ContentHtmlDTO, IContentHtmlService>
    {
        public ContentHtmlController(IContentHtmlService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            :base(service,mapper, logFactory, emailService)
        {

        }
    }
}
