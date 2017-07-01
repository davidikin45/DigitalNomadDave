using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using Solution.Base.Controllers;
using Solution.Base.Controllers.Api;
using Solution.Base.Email;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DND.Controllers.Api
{
    [RoutePrefix("api/blog-post")]
    public class BlogPostController : BaseEntityWebApiControllerAuthorize<BlogPostDTO,IBlogPostService>
    {
        public BlogPostController(IBlogPostService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            :base(service,mapper, logFactory, emailService)
        {

        }
    }
}
