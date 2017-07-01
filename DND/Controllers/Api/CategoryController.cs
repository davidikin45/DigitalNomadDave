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
    [RoutePrefix("api/category")]
    public class CategoryController : BaseEntityWebApiControllerAuthorize<CategoryDTO,ICategoryService>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logFactory">The log factory.</param>
        public CategoryController(ICategoryService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            :base(service,mapper, logFactory, emailService)
        {

        }      
    }
}
