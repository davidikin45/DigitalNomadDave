using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using HtmlTags;
using Solution.Base.Controllers;
using Solution.Base.Controllers.Api;
using Solution.Base.Email;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DND.Controllers.Api
{
    [RoutePrefix("api/tag")]
    public class TagController : BaseEntityWebApiControllerAuthorize<TagDTO,ITagService>
    {
        public TagController(ITagService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            : base(service, mapper, logFactory, emailService)
        {

        }

    }
}
