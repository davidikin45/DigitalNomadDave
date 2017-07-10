using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using DND.Core.Models;
using Solution.Base.Controllers;
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

namespace DND.Controllers.Admin
{
    [RoutePrefix("admin/location")]
    public class AdminLocationController : BaseEntityControllerAuthorize<LocationDTO, ILocationService>
    {
        public AdminLocationController(ILocationService service, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
             : base(true, service, mapper, logFactory, emailService)
        {
        }

    }
}
