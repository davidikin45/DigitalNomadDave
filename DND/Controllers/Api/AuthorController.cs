using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using DND.Core.Models;
using Solution.Base.Controllers.Api;
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

namespace DND.Controllers.Api
{
    [RoutePrefix("api/author")]
    public class AuthorController : BaseEntityWebApiControllerAuthorize<AuthorDTO, IAuthorService>
    {
        public AuthorController(IAuthorService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
            :base(service, mapper, logFactory, emailService)
        {

        }
    }
}
